namespace Omemsis
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.btnHelpMe = new System.Windows.Forms.Button();
            this.groupTools = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnShowHud = new System.Windows.Forms.Button();
            this.btnHideHud = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnPatchEditor = new System.Windows.Forms.Button();
            this.btnHaloClick = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.groupTools.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(217, 243);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 3;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // btnHelpMe
            // 
            this.btnHelpMe.Location = new System.Drawing.Point(102, 237);
            this.btnHelpMe.Name = "btnHelpMe";
            this.btnHelpMe.Size = new System.Drawing.Size(75, 37);
            this.btnHelpMe.TabIndex = 8;
            this.btnHelpMe.Text = "Doesn\'t Work?";
            this.btnHelpMe.UseVisualStyleBackColor = true;
            this.btnHelpMe.Click += new System.EventHandler(this.btnIssues_Click);
            // 
            // groupTools
            // 
            this.groupTools.Controls.Add(this.label3);
            this.groupTools.Controls.Add(this.label2);
            this.groupTools.Controls.Add(this.btnShowHud);
            this.groupTools.Controls.Add(this.btnHideHud);
            this.groupTools.Location = new System.Drawing.Point(12, 12);
            this.groupTools.Name = "groupTools";
            this.groupTools.Size = new System.Drawing.Size(171, 219);
            this.groupTools.TabIndex = 9;
            this.groupTools.TabStop = false;
            this.groupTools.Text = "Experimental";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 91);
            this.label2.TabIndex = 2;
            this.label2.Text = "Note:\r\nSpeed is a bit slow because\r\nwe are doing pattern scans.\r\n\r\nWe will fix th" +
    "e slowness in \r\nnewer releases. Until then, \r\nthis might be slow!";
            // 
            // btnShowHud
            // 
            this.btnShowHud.Location = new System.Drawing.Point(9, 20);
            this.btnShowHud.Name = "btnShowHud";
            this.btnShowHud.Size = new System.Drawing.Size(75, 23);
            this.btnShowHud.TabIndex = 1;
            this.btnShowHud.Text = "Show Hud";
            this.btnShowHud.UseVisualStyleBackColor = true;
            this.btnShowHud.Click += new System.EventHandler(this.btnShowHud_Click);
            // 
            // btnHideHud
            // 
            this.btnHideHud.Location = new System.Drawing.Point(90, 20);
            this.btnHideHud.Name = "btnHideHud";
            this.btnHideHud.Size = new System.Drawing.Size(75, 23);
            this.btnHideHud.TabIndex = 0;
            this.btnHideHud.Text = "Hide Hud";
            this.btnHideHud.UseVisualStyleBackColor = true;
            this.btnHideHud.Click += new System.EventHandler(this.btnHideHud_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnPatchEditor);
            this.groupBox1.Location = new System.Drawing.Point(12, 280);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(174, 57);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Developer Tools";
            // 
            // btnPatchEditor
            // 
            this.btnPatchEditor.Location = new System.Drawing.Point(9, 22);
            this.btnPatchEditor.Name = "btnPatchEditor";
            this.btnPatchEditor.Size = new System.Drawing.Size(156, 23);
            this.btnPatchEditor.TabIndex = 0;
            this.btnPatchEditor.Text = "Patch Editor";
            this.btnPatchEditor.UseVisualStyleBackColor = true;
            this.btnPatchEditor.Click += new System.EventHandler(this.btnPatchEditor_click);
            // 
            // btnHaloClick
            // 
            this.btnHaloClick.Location = new System.Drawing.Point(21, 237);
            this.btnHaloClick.Name = "btnHaloClick";
            this.btnHaloClick.Size = new System.Drawing.Size(75, 37);
            this.btnHaloClick.TabIndex = 4;
            this.btnHaloClick.Text = "Github";
            this.btnHaloClick.UseVisualStyleBackColor = true;
            this.btnHaloClick.Click += new System.EventHandler(this.btnHaloClick_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(158, 52);
            this.label3.TabIndex = 15;
            this.label3.Text = "If you want to change memory \r\nat run time please click the \r\npatch editor and do" +
    " \r\nwhat you need to do.";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(196, 346);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnHaloClick);
            this.Controls.Add(this.groupTools);
            this.Controls.Add(this.btnHelpMe);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Omemsis";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupTools.ResumeLayout(false);
            this.groupTools.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnHelpMe;
        private System.Windows.Forms.GroupBox groupTools;
        private System.Windows.Forms.Button btnHideHud;
        private System.Windows.Forms.Button btnShowHud;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnPatchEditor;
        private System.Windows.Forms.Button btnHaloClick;
        private System.Windows.Forms.Label label3;
    }
}

