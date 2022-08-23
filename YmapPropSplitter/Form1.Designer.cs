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
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
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
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
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
            resources.ApplyResources(this.btnSplit, "btnSplit");
            this.btnSplit.Name = "btnSplit";
            this.btnSplit.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // btnBrowseOutput
            // 
            resources.ApplyResources(this.btnBrowseOutput, "btnBrowseOutput");
            this.btnBrowseOutput.Name = "btnBrowseOutput";
            this.btnBrowseOutput.UseVisualStyleBackColor = true;
            // 
            // btnBrowseYMAP
            // 
            resources.ApplyResources(this.btnBrowseYMAP, "btnBrowseYMAP");
            this.btnBrowseYMAP.Name = "btnBrowseYMAP";
            this.btnBrowseYMAP.UseVisualStyleBackColor = true;
            // 
            // btnBrowseYTYP
            // 
            resources.ApplyResources(this.btnBrowseYTYP, "btnBrowseYTYP");
            this.btnBrowseYTYP.Name = "btnBrowseYTYP";
            this.btnBrowseYTYP.UseVisualStyleBackColor = true;
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
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
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
    }
}