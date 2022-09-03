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
            System.Windows.Forms.Button btnMoveTracks;
            System.Windows.Forms.Button btnNavConvert;
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
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
            this.lbTrainTrack = new System.Windows.Forms.Label();
            this.tbMoveZ = new System.Windows.Forms.TextBox();
            this.tbMoveY = new System.Windows.Forms.TextBox();
            this.tbMoveX = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnTrackOutputBrowse = new System.Windows.Forms.Button();
            this.btnBrowseTracks = new System.Windows.Forms.Button();
            this.tbOutputTracks = new System.Windows.Forms.TextBox();
            this.tbTrainTracksIn = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnOnvOutput = new System.Windows.Forms.Button();
            this.btnYnvInput = new System.Windows.Forms.Button();
            this.tbOnvOutput = new System.Windows.Forms.TextBox();
            this.tbYNVs = new System.Windows.Forms.TextBox();
            btnMerge = new System.Windows.Forms.Button();
            btnMoveTracks = new System.Windows.Forms.Button();
            btnNavConvert = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnMerge
            // 
            btnMerge.BackColor = System.Drawing.Color.YellowGreen;
            btnMerge.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(btnMerge, "btnMerge");
            btnMerge.ForeColor = System.Drawing.Color.White;
            btnMerge.Name = "btnMerge";
            btnMerge.UseVisualStyleBackColor = false;
            btnMerge.Click += new System.EventHandler(this.btnMerge_Click);
            // 
            // btnMoveTracks
            // 
            btnMoveTracks.BackColor = System.Drawing.Color.YellowGreen;
            btnMoveTracks.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(btnMoveTracks, "btnMoveTracks");
            btnMoveTracks.ForeColor = System.Drawing.Color.White;
            btnMoveTracks.Name = "btnMoveTracks";
            btnMoveTracks.UseVisualStyleBackColor = false;
            btnMoveTracks.Click += new System.EventHandler(this.btnMoveTracks_Click);
            // 
            // btnNavConvert
            // 
            btnNavConvert.BackColor = System.Drawing.Color.YellowGreen;
            btnNavConvert.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(btnNavConvert, "btnNavConvert");
            btnNavConvert.ForeColor = System.Drawing.Color.White;
            btnNavConvert.Name = "btnNavConvert";
            btnNavConvert.UseVisualStyleBackColor = false;
            btnNavConvert.Click += new System.EventHandler(this.btnNavConvert_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.WindowFrame;
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
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
            this.tabPage1.ForeColor = System.Drawing.Color.YellowGreen;
            this.tabPage1.Name = "tabPage1";
            // 
            // lbYmap
            // 
            resources.ApplyResources(this.lbYmap, "lbYmap");
            this.lbYmap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbYmap.Name = "lbYmap";
            // 
            // lbYTYPstatus
            // 
            resources.ApplyResources(this.lbYTYPstatus, "lbYTYPstatus");
            this.lbYTYPstatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbYTYPstatus.Name = "lbYTYPstatus";
            // 
            // btnSplit
            // 
            this.btnSplit.BackColor = System.Drawing.Color.YellowGreen;
            this.btnSplit.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btnSplit, "btnSplit");
            this.btnSplit.ForeColor = System.Drawing.Color.White;
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
            this.tabPage2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
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
            // 
            // lbYmapMerg
            // 
            resources.ApplyResources(this.lbYmapMerg, "lbYmapMerg");
            this.lbYmapMerg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbYmapMerg.Name = "lbYmapMerg";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.ForeColor = System.Drawing.Color.White;
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
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Name = "label5";
            // 
            // btnBrowseOutputM
            // 
            this.btnBrowseOutputM.BackColor = System.Drawing.Color.LightCoral;
            this.btnBrowseOutputM.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btnBrowseOutputM, "btnBrowseOutputM");
            this.btnBrowseOutputM.ForeColor = System.Drawing.Color.White;
            this.btnBrowseOutputM.Name = "btnBrowseOutputM";
            this.btnBrowseOutputM.UseVisualStyleBackColor = false;
            this.btnBrowseOutputM.Click += new System.EventHandler(this.btnBrowseOutputM_Click);
            // 
            // btnBrowseYmapM
            // 
            this.btnBrowseYmapM.BackColor = System.Drawing.Color.ForestGreen;
            this.btnBrowseYmapM.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btnBrowseYmapM, "btnBrowseYmapM");
            this.btnBrowseYmapM.ForeColor = System.Drawing.Color.White;
            this.btnBrowseYmapM.Name = "btnBrowseYmapM";
            this.btnBrowseYmapM.UseVisualStyleBackColor = false;
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
            this.tabPage3.BackColor = System.Drawing.Color.DimGray;
            this.tabPage3.Controls.Add(this.lbTrainTrack);
            this.tabPage3.Controls.Add(this.tbMoveZ);
            this.tabPage3.Controls.Add(this.tbMoveY);
            this.tabPage3.Controls.Add(this.tbMoveX);
            this.tabPage3.Controls.Add(this.button4);
            this.tabPage3.Controls.Add(this.button6);
            this.tabPage3.Controls.Add(this.button5);
            this.tabPage3.Controls.Add(btnMoveTracks);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.btnTrackOutputBrowse);
            this.tabPage3.Controls.Add(this.btnBrowseTracks);
            this.tabPage3.Controls.Add(this.tbOutputTracks);
            this.tabPage3.Controls.Add(this.tbTrainTracksIn);
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Name = "tabPage3";
            // 
            // lbTrainTrack
            // 
            resources.ApplyResources(this.lbTrainTrack, "lbTrainTrack");
            this.lbTrainTrack.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbTrainTrack.Name = "lbTrainTrack";
            // 
            // tbMoveZ
            // 
            resources.ApplyResources(this.tbMoveZ, "tbMoveZ");
            this.tbMoveZ.Name = "tbMoveZ";
            this.tbMoveZ.TextChanged += new System.EventHandler(this.tbMoveZ_TextChanged);
            // 
            // tbMoveY
            // 
            resources.ApplyResources(this.tbMoveY, "tbMoveY");
            this.tbMoveY.Name = "tbMoveY";
            this.tbMoveY.TextChanged += new System.EventHandler(this.tbMoveY_TextChanged);
            // 
            // tbMoveX
            // 
            resources.ApplyResources(this.tbMoveX, "tbMoveX");
            this.tbMoveX.Name = "tbMoveX";
            this.tbMoveX.TextChanged += new System.EventHandler(this.tbMoveX_TextChanged);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.IndianRed;
            resources.ApplyResources(this.button4, "button4");
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Name = "button4";
            this.button4.UseVisualStyleBackColor = false;
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.SteelBlue;
            resources.ApplyResources(this.button6, "button6");
            this.button6.FlatAppearance.BorderSize = 0;
            this.button6.ForeColor = System.Drawing.Color.White;
            this.button6.Name = "button6";
            this.button6.UseVisualStyleBackColor = false;
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.LimeGreen;
            resources.ApplyResources(this.button5, "button5");
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button5.Name = "button5";
            this.button5.UseVisualStyleBackColor = false;
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Name = "label7";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Name = "label8";
            // 
            // btnTrackOutputBrowse
            // 
            this.btnTrackOutputBrowse.BackColor = System.Drawing.Color.LightCoral;
            this.btnTrackOutputBrowse.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btnTrackOutputBrowse, "btnTrackOutputBrowse");
            this.btnTrackOutputBrowse.ForeColor = System.Drawing.Color.White;
            this.btnTrackOutputBrowse.Name = "btnTrackOutputBrowse";
            this.btnTrackOutputBrowse.UseVisualStyleBackColor = false;
            this.btnTrackOutputBrowse.Click += new System.EventHandler(this.btnTrackOutputBrowse_Click);
            // 
            // btnBrowseTracks
            // 
            this.btnBrowseTracks.BackColor = System.Drawing.Color.ForestGreen;
            this.btnBrowseTracks.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btnBrowseTracks, "btnBrowseTracks");
            this.btnBrowseTracks.ForeColor = System.Drawing.Color.White;
            this.btnBrowseTracks.Name = "btnBrowseTracks";
            this.btnBrowseTracks.UseVisualStyleBackColor = false;
            this.btnBrowseTracks.Click += new System.EventHandler(this.btnBrowseTracks_Click);
            // 
            // tbOutputTracks
            // 
            resources.ApplyResources(this.tbOutputTracks, "tbOutputTracks");
            this.tbOutputTracks.Name = "tbOutputTracks";
            // 
            // tbTrainTracksIn
            // 
            resources.ApplyResources(this.tbTrainTracksIn, "tbTrainTracksIn");
            this.tbTrainTracksIn.Name = "tbTrainTracksIn";
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.DimGray;
            this.tabPage4.Controls.Add(btnNavConvert);
            this.tabPage4.Controls.Add(this.label10);
            this.tabPage4.Controls.Add(this.label11);
            this.tabPage4.Controls.Add(this.btnOnvOutput);
            this.tabPage4.Controls.Add(this.btnYnvInput);
            this.tabPage4.Controls.Add(this.tbOnvOutput);
            this.tabPage4.Controls.Add(this.tbYNVs);
            resources.ApplyResources(this.tabPage4, "tabPage4");
            this.tabPage4.Name = "tabPage4";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Name = "label10";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Name = "label11";
            // 
            // btnOnvOutput
            // 
            this.btnOnvOutput.BackColor = System.Drawing.Color.LightCoral;
            this.btnOnvOutput.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btnOnvOutput, "btnOnvOutput");
            this.btnOnvOutput.ForeColor = System.Drawing.Color.White;
            this.btnOnvOutput.Name = "btnOnvOutput";
            this.btnOnvOutput.UseVisualStyleBackColor = false;
            this.btnOnvOutput.Click += new System.EventHandler(this.btnOnvOutput_Click);
            // 
            // btnYnvInput
            // 
            this.btnYnvInput.BackColor = System.Drawing.Color.ForestGreen;
            this.btnYnvInput.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btnYnvInput, "btnYnvInput");
            this.btnYnvInput.ForeColor = System.Drawing.Color.White;
            this.btnYnvInput.Name = "btnYnvInput";
            this.btnYnvInput.UseVisualStyleBackColor = false;
            this.btnYnvInput.Click += new System.EventHandler(this.btnYnvInput_Click);
            // 
            // tbOnvOutput
            // 
            resources.ApplyResources(this.tbOnvOutput, "tbOnvOutput");
            this.tbOnvOutput.Name = "tbOnvOutput";
            // 
            // tbYNVs
            // 
            resources.ApplyResources(this.tbYNVs, "tbYNVs");
            this.tbYNVs.Name = "tbYNVs";
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
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
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
        private Label label7;
        private Label label8;
        private Button btnTrackOutputBrowse;
        private Button btnBrowseTracks;
        private TextBox tbOutputTracks;
        private TextBox tbTrainTracksIn;
        private Button button5;
        private Button button6;
        private Button button4;
        private TextBox tbMoveZ;
        private TextBox tbMoveY;
        private TextBox tbMoveX;
        private Label lbTrainTrack;
        private TabPage tabPage4;
        private Label label10;
        private Label label11;
        private Button btnOnvOutput;
        private Button btnYnvInput;
        private TextBox tbOnvOutput;
        private TextBox tbYNVs;
    }
}