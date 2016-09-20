using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SigScan.Classes;
using System.Net;
using Newtonsoft.Json;
namespace Omemsis
{
    public partial class MainForm : Form
    {
        public static Process HaloOnline;
        public static bool HaloIsRunning = true;

        //Lets keep the previous scan in memory so only the first scan is slow.
        IntPtr pAddr;
        IntPtr MpPatchAddr;

        bool WeRunningYup = false;
       
        bool forceLoading = false;

        string HaloOnlineEXE = "halo5forge";

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LogFile.WriteToLog("------------- Started Omemsis -------------");
            

            Thread loadPatches = new Thread(MagicPatches.LoadPatches);
            loadPatches.Start();
            WeRunningYup = true;

       

            //Let's keep an eye out for Halo starting and stopping.
            Thread haloWatcher = new Thread(IsHaloRunning);
            haloWatcher.Start();

            //Let's keep an eye out for frost so we can kill it and hijack the session tokens

            //Let's make sure people are running the greatest latest turd available
            if (!Program.IsDebug)
            {
                Thread checkUpdates = new Thread(CheckForUpdates);
                checkUpdates.Start();
            }
        }
        bool inLauncherLoop = false;


        private void CheckForUpdates()
        {
            
            var url = "https://raw.githubusercontent.com/no1dead/Omemsis/master/Omemsis-Versions.json";
            try
            {
                var versionJson = (new WebClient()).DownloadString(url);
                FileVersions.NewFiles = JsonConvert.DeserializeObject<FileVersions.Files>(versionJson);
                FileVersions.OldFiles = JsonConvert.DeserializeObject<FileVersions.Files>(File.ReadAllText("Omemsis-Versions.json"));
                FileVersions.File file = FileVersions.FindNewByFilename("Omemsis.exe");
                Version newVersion = Version.Parse(file.version);
                Version currentVersion = Version.Parse(Application.ProductVersion);
                if (currentVersion < newVersion)
                {
                    this.Invoke(new MethodInvoker(delegate { this.Text = "Omemsis - Update Available!"; }));

                    DialogResult result1 = MessageBox.Show("There's a new version of Omemsis available. Would you like to download it?", "Oh goody!", MessageBoxButtons.YesNo);
                    if (result1 == DialogResult.Yes)
                    {
                        Process.Start(file.url);
                    }
                }

                FileVersions.File patchesNew = FileVersions.FindNewByFilename("Omemsis-Patches.json");
                FileVersions.File patchesOld = FileVersions.FindOldByFilename("Omemsis-Patches.json");

                if (Convert.ToInt32(patchesNew.version) > Convert.ToInt32(patchesOld.version))
                {
                    DialogResult result1 = MessageBox.Show("There's new patches available for Omemsis. Would you like to download them?", "Oh goody!", MessageBoxButtons.YesNo);
                    if (result1 == DialogResult.Yes)
                    {
                        DialogResult dialogResult = MessageBox.Show("Are you sure you want to download the latest Patch File? This will overwrite any changes you've made! If you haven't made any, you'll be fine. If you have, please backup your changes before hitting OK.", "Replace Patches?", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            Program.GetLatestPatchJson(true);
                        }
                    }
                }
            }
            catch (Exception e) {
                
            }
        }
        private void IsHaloRunning()
        {
            while (WeRunningYup)
            {
                try
                {
                    if (!HaloOnline.HasExited)
                    {
                        HaloIsRunning = true;
                    }
                    else
                    {
                        
                        HaloIsRunning = false;
                        HaloOnline = null;
                        pAddr = new IntPtr(0);
                        MpPatchAddr = new IntPtr(0);
                    }
                }
                catch (Exception)
                {
                    //Let's just not
                }
                Thread.Sleep(200);
            }
        }
        private void btnHaloClick_Click(object sender, EventArgs e)
        {
            
            Process.Start("http://github.com/no1dead/Omemsis", "");
        }

        IntPtr PtrMapName;
        IntPtr PtrMapReset;
        IntPtr PtrMapTime;
        IntPtr PtrMapType;
        IntPtr PtrGameType;
        IntPtr PtrMpPatch;

        private void btnDarkLoad_Click(object sender, EventArgs e)
        {
            
            Thread forceLoadMap = new Thread(ForceLoadMap);
            forceLoadMap.Start();
        }

        private void ForceLoadMap()
        {
            //Do Magic
            try
            {
                IntPtr p = Memory.OpenProcess(0x001F0FFF, true, HaloOnline.Id);

                if (pAddr == null || pAddr.ToInt32() == 0)
                {
                    pAddr = MagicPatches.ScanForPattern(HaloOnline, new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x6D, 0x61, 0x70, 0x73, 0x5C, 0x6D, 0x61, 0x69, 0x6E, 0x6D, 0x65, 0x6E, 0x75 }, "xxxxxxxxxxxxxxxxxxxxx", 0);

                    if (pAddr == null || pAddr.ToInt32() <= 0)
                    {
                        MessageBox.Show("Omemsis failed to find the map loading code...\nThis could mean two things:\n\n1. You tried loading a map already and closed + opened HaloLoader on the same EXE (You have to keep it running!)\n2. This version of Halo Online is not supported.");
                        return;
                    }
                    //pAddr = 4BB300C
                    PtrMapName = pAddr + 0xD;    //0x4BB3019
                    PtrMapReset = pAddr - 0x2C;  //0x4BB2FE0
                    PtrMapTime = pAddr + 0x435;  //0x4BB3441
                    PtrMapType = pAddr - 0x1C;   //0x4BB2FF0
                    PtrGameType = PtrMapReset + 0x33C;  //0x4BB331C
                }
                //Pattern Scan Finds (4BB300C)
                //Map Title = Mainmenu (04BB3019)

                int lpNumberOfBytesWritten = 0;


                //Patch Multiplayer Loading
                /*
                 *   eldorado.Scaleform::Event::TryAcquireCancel+155147 - 8B CE                 - mov ecx,esi
                 *   eldorado.Scaleform::Event::TryAcquireCancel+155149 - 66 89 43 02           - mov [ebx+02],ax
                 *   eldorado.Scaleform::Event::TryAcquireCancel+15514D - E8 9E150000           - call eldorado.Scaleform::Event::TryAcquireCancel+1566F0
                 *   eldorado.Scaleform::Event::TryAcquireCancel+155152 - 8B 47 10              - mov eax,[edi+10]
                 *
                 * */

                if (MpPatchAddr == null || MpPatchAddr.ToInt32() <= 0)
                {
                    //New builds of Halo Online

                    MpPatchAddr = MagicPatches.ScanForPattern(HaloOnline, new byte[] { 0x8B, 0xCE, 0x66, 0x89, 0x43, 0x02, 0xE8, 0x9E, 0x15, 0x00, 0x00, 0x8B, 0x47, 0x10 }, "xxxxx??????xxx", 7);

                    if (MpPatchAddr == null || MpPatchAddr.ToInt32() <= 0)
                    {
                        //Original Halo Online - Eldewrito!
                        MpPatchAddr = MagicPatches.ScanForPattern(HaloOnline, new byte[] { 0x17, 0x56, 0x66, 0x89, 0x47, 0x02, 0xE8, 0x4C, 0xFB, 0xFF, 0xFF, 0x57, 0x53, 0x56 }, "xxxxx??????xxx", 7);
                    }

                    PtrMpPatch = MpPatchAddr - 0x1;
                }

                //Pointer finds 0xF605FE - actual is 0xF605FD

                if (PtrMpPatch.ToInt32() <= 0)
                {
                    
                    MessageBox.Show("Failed to find pointer... Go file a bug report.");
                    return;

                }
                
                byte[] nop = { 0x90, 0x90, 0x90, 0x90, 0x90 };
                Memory.WriteProcessMemory(p, PtrMpPatch, nop, 5, out lpNumberOfBytesWritten);

                byte[] mapReset = { 0x1 };
                // sets map type
                byte[] mapType = { 0, 0, 0, 0 };
                // sets gametype
                byte[] gameType = { 0, 0, 0, 0 };
                // Infinite play time
                byte[] mapTime = { 0x0 };
                // Grab map name from selected listbox
                byte[] mapName = new byte[36];
                Memory.WriteProcessMemory(p, PtrGameType, gameType, 4, out lpNumberOfBytesWritten);
                Memory.WriteProcessMemory(p, PtrMapType, mapType, 4, out lpNumberOfBytesWritten);
                Memory.WriteProcessMemory(p, PtrMapName, mapName, mapName.Length, out lpNumberOfBytesWritten);
                Memory.WriteProcessMemory(p, PtrMapTime, mapTime, 1, out lpNumberOfBytesWritten);
                Memory.WriteProcessMemory(p, PtrMapReset, mapReset, 1, out lpNumberOfBytesWritten);
            }
            catch (Exception ex)
            {
                
                MessageBox.Show("Something went wrong...\n" + ex.Message, "Omemsis Error");
            }

            forceLoading = false;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            WeRunningYup = false;
        }
        Forms.Splash splash;
        private void btnLaunchHaloOnline_Click(object sender, EventArgs e)
        {

            
            splash = new Forms.Splash();
            splash.Show();
            Thread startHalo = new Thread(LaunchHaloOnline);
            startHalo.Start();
        }

        private void LaunchHaloOnline()
        {
        }

        private void btnIssues_Click(object sender, EventArgs e)
        {
            
            Process.Start("https://github.com/no1dead/Omemsis/issues", "");
        }

        private void btnHideHud_Click(object sender, EventArgs e)
        {
            
            btnHideHud.Text = "Scanning";
            btnHideHud.Enabled = false;

            MagicPatches.RunPatch("Hud Hide");

            btnHideHud.Text = "Hide Hud";
            btnHideHud.Enabled = true;
        }

        private void btnShowHud_Click(object sender, EventArgs e)
        {
            
            btnShowHud.Text = "Scanning";
            btnShowHud.Enabled = false;

            MagicPatches.RunPatch("Hud Show");
            
            btnShowHud.Text = "Show Hud";
            btnShowHud.Enabled = true;

        }

        PatchEditor patchy;
        private void btnPatchEditor_click(object sender, EventArgs e)
        {
            
            if (!PatchEditor.FormShowing)
            {
                patchy = new PatchEditor();
                patchy.Show();
            }
            else
            {
                patchy.Focus();
            }
        }

        private void btn4gamePlay_Click(object sender, EventArgs e)
        {
           
            MessageBox.Show("Login to 4game and hit Play like you would normally load Halo Online. Omemsis will catch it and DarkLoad so you can play online.");
            Process.Start("https://ru.4game.com/halo/play/");
        }

        private void comboGameTypes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboGameModes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtHaloLaunchArguments_TextChanged(object sender, EventArgs e)
        {
        }

        private void listMapInfo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}