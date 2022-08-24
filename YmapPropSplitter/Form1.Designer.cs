namespace YmapPropSplitter
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Button btnMerge;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lbYmap = new System.Windows.Forms.Label();
            this.lbYTYPstatus = new System.Windows.Forms.Label();
            this.btnSplit = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBrowseOutput = new System.Windows.Forms.Button();
            this.btnBrowseYMAP = new System.Windows.Forms.Button();
            this.btnBrowseYTYP = new System.Windows.Forms.Button();
            this.tbOutput = new System.Windows.Forms.TextBox();
            this.tbYMAP = new System.Windows.Forms.TextBox();
            this.tbYTYP = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lbYmapMerg = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbYmapName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnBrowseOutputM = new System.Windows.Forms.Button();
            this.btnBrowseYmapM = new System.Windows.Forms.Button();
            this.tbOutputM = new System.Windows.Forms.TextBox();
            this.tbYmapM = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            btnMerge = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnMerge
            // 
            resources.ApplyResources(btnMerge, "btnMerge");
            btnMerge.Name = "btnMerge";
            btnMerge.UseVisualStyleBackColor = true;
            btnMerge.Click += new System.EventHandler(this.btnMerge_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.WindowFrame;
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Controls.Add(this.progressBar1);
            this.tabPage1.Controls.Add(this.lbYmap);
            this.tabPage1.Controls.Add(this.lbYTYPstatus);
            this.tabPage1.Controls.Add(this.btnSplit);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.btnBrowseOutput);
            this.tabPage1.Controls.Add(this.btnBrowseYMAP);
            this.tabPage1.Controls.Add(this.btnBrowseYTYP);
            this.tabPage1.Controls.Add(this.tbOutput);
            this.tabPage1.Controls.Add(this.tbYMAP);
            this.tabPage1.Controls.Add(this.tbYTYP);
            this.tabPage1.ForeColor = System.Drawing.Color.Cornsilk;
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // progressBar1
            // 
            resources.ApplyResources(this.progressBar1, "progressBar1");
            this.progressBar1.Name = "progressBar1";
            // 
            // lbYmap
            // 
            resources.ApplyResources(this.lbYmap, "lbYmap");
            this.lbYmap.ForeColor = System.Drawing.Color.IndianRed;
            this.lbYmap.Name = "lbYmap";
            // 
            // lbYTYPstatus
            // 
            resources.ApplyResources(this.lbYTYPstatus, "lbYTYPstatus");
            this.lbYTYPstatus.ForeColor = System.Drawing.Color.IndianRed;
            this.lbYTYPstatus.Name = "lbYTYPstatus";
            // 
            // btnSplit
            // 
            this.btnSplit.BackColor = System.Drawing.Color.YellowGreen;
            this.btnSplit.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.btnSplit.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btnSplit, "btnSplit");
            this.btnSplit.ForeColor = System.Drawing.Color.Indigo;
            this.btnSplit.Name = "btnSplit";
            this.btnSplit.UseVisualStyleBackColor = false;
            this.btnSplit.Click += new System.EventHandler(this.btnSplit_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Name = "label1";
            // 
            // btnBrowseOutput
            // 
            this.btnBrowseOutput.BackColor = System.Drawing.Color.LightCoral;
            this.btnBrowseOutput.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btnBrowseOutput, "btnBrowseOutput");
            this.btnBrowseOutput.ForeColor = System.Drawing.Color.White;
            this.btnBrowseOutput.Name = "btnBrowseOutput";
            this.btnBrowseOutput.UseVisualStyleBackColor = false;
            this.btnBrowseOutput.Click += new System.EventHandler(this.btnBrowseOutput_Click);
            // 
            // btnBrowseYMAP
            // 
            this.btnBrowseYMAP.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btnBrowseYMAP.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btnBrowseYMAP, "btnBrowseYMAP");
            this.btnBrowseYMAP.Name = "btnBrowseYMAP";
            this.btnBrowseYMAP.UseVisualStyleBackColor = false;
            this.btnBrowseYMAP.Click += new System.EventHandler(this.btnBrowseYMAP_Click);
            // 
            // btnBrowseYTYP
            // 
            this.btnBrowseYTYP.BackColor = System.Drawing.Color.ForestGreen;
            this.btnBrowseYTYP.FlatAppearance.BorderColor = System.Drawing.Color.RosyBrown;
            this.btnBrowseYTYP.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btnBrowseYTYP, "btnBrowseYTYP");
            this.btnBrowseYTYP.ForeColor = System.Drawing.Color.White;
            this.btnBrowseYTYP.Name = "btnBrowseYTYP";
            this.btnBrowseYTYP.UseVisualStyleBackColor = false;
            this.btnBrowseYTYP.Click += new System.EventHandler(this.btnBrowseYTYP_Click);
            // 
            // tbOutput
            // 
            resources.ApplyResources(this.tbOutput, "tbOutput");
            this.tbOutput.Name = "tbOutput";
            // 
            // tbYMAP
            // 
            resources.ApplyResources(this.tbYMAP, "tbYMAP");
            this.tbYMAP.Name = "tbYMAP";
            // 
            // tbYTYP
            // 
            resources.ApplyResources(this.tbYTYP, "tbYTYP");
            this.tbYTYP.Name = "tbYTYP";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lbYmapMerg);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.tbYmapName);
            this.tabPage2.Controls.Add(btnMerge);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.btnBrowseOutputM);
            this.tabPage2.Controls.Add(this.btnBrowseYmapM);
            this.tabPage2.Controls.Add(this.tbOutputM);
            this.tabPage2.Controls.Add(this.tbYmapM);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lbYmapMerg
            // 
            resources.ApplyResources(this.lbYmapMerg, "lbYmapMerg");
            this.lbYmapMerg.ForeColor = System.Drawing.Color.IndianRed;
            this.lbYmapMerg.Name = "lbYmapMerg";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // tbYmapName
            // 
            resources.ApplyResources(this.tbYmapName, "tbYmapName");
            this.tbYmapName.Name = "tbYmapName";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // btnBrowseOutputM
            // 
            resources.ApplyResources(this.btnBrowseOutputM, "btnBrowseOutputM");
            this.btnBrowseOutputM.Name = "btnBrowseOutputM";
            this.btnBrowseOutputM.UseVisualStyleBackColor = true;
            this.btnBrowseOutputM.Click += new System.EventHandler(this.btnBrowseOutputM_Click);
            // 
            // btnBrowseYmapM
            // 
            resources.ApplyResources(this.btnBrowseYmapM, "btnBrowseYmapM");
            this.btnBrowseYmapM.Name = "btnBrowseYmapM";
            this.btnBrowseYmapM.UseVisualStyleBackColor = true;
            this.btnBrowseYmapM.Click += new System.EventHandler(this.btnBrowseYmapM_Click);
            // 
            // tbOutputM
            // 
            resources.ApplyResources(this.tbOutputM, "tbOutputM");
            this.tbOutputM.Name = "tbOutputM";
            // 
            // tbYmapM
            // 
            resources.ApplyResources(this.tbYmapM, "tbYmapM");
            this.tbYmapM.Name = "tbYmapM";
            // 
            // tabPage3
            // 
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private ProgressBar progressBar1;
        private Label lbYmap;
        private Label lbYTYPstatus;
        private Button btnSplit;
        private Label label3;
        private Label label2;
        private Label label1;
        private Button btnBrowseOutput;
        private Button btnBrowseYMAP;
        private Button btnBrowseYTYP;
        private TextBox tbOutput;
        private TextBox tbYMAP;
        private TextBox tbYTYP;
        private TabPage tabPage2;
        private Label label6;
        private TextBox tbYmapName;
        private Label label4;
        private Label label5;
        private Button btnBrowseOutputM;
        private Button btnBrowseYmapM;
        private TextBox tbOutputM;
        private TextBox tbYmapM;
        private Label lbYmapMerg;
        private TabPage tabPage3;
    }
}