using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omemsis
{
    class LogFile
    {
        public static void WriteToLog(string text)
        {
            using (StreamWriter sw = File.AppendText("Omemsis.log"))
            {
                sw.WriteLine(text);
            }
        }
    }
}