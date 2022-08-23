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
            this.tbYTYP = new System.Windows.Forms.TextBox();
            this.tbYMAP = new System.Windows.Forms.TextBox();
            this.btnBrowseYTYP = new System.Windows.Forms.Button();
            this.btnBrowseYMAP = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbOutput = new System.Windows.Forms.TextBox();
            this.btnBrowseOutput = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSplit = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lbYTYPstatus = new System.Windows.Forms.Label();
            this.lbYmap = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbYTYP
            // 
            resources.ApplyResources(this.tbYTYP, "tbYTYP");
            this.tbYTYP.Name = "tbYTYP";
            this.tbYTYP.TextChanged += new System.EventHandler(this.tbYTYP_TextChanged);
            // 
            // tbYMAP
            // 
            resources.ApplyResources(this.tbYMAP, "tbYMAP");
            this.tbYMAP.Name = "tbYMAP";
            // 
            // btnBrowseYTYP
            // 
            resources.ApplyResources(this.btnBrowseYTYP, "btnBrowseYTYP");
            this.btnBrowseYTYP.Name = "btnBrowseYTYP";
            this.btnBrowseYTYP.UseVisualStyleBackColor = true;
            this.btnBrowseYTYP.Click += new System.EventHandler(this.btnBrowseYTYP_Click);
            // 
            // btnBrowseYMAP
            // 
            resources.ApplyResources(this.btnBrowseYMAP, "btnBrowseYMAP");
            this.btnBrowseYMAP.Name = "btnBrowseYMAP";
            this.btnBrowseYMAP.UseVisualStyleBackColor = true;
            this.btnBrowseYMAP.Click += new System.EventHandler(this.btnBrowseYMAP_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // tbOutput
            // 
            resources.ApplyResources(this.tbOutput, "tbOutput");
            this.tbOutput.Name = "tbOutput";
            // 
            // btnBrowseOutput
            // 
            resources.ApplyResources(this.btnBrowseOutput, "btnBrowseOutput");
            this.btnBrowseOutput.Name = "btnBrowseOutput";
            this.btnBrowseOutput.UseVisualStyleBackColor = true;
            this.btnBrowseOutput.Click += new System.EventHandler(this.btnBrowseOutput_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // btnSplit
            // 
            resources.ApplyResources(this.btnSplit, "btnSplit");
            this.btnSplit.Name = "btnSplit";
            this.btnSplit.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // lbYTYPstatus
            // 
            resources.ApplyResources(this.lbYTYPstatus, "lbYTYPstatus");
            this.lbYTYPstatus.ForeColor = System.Drawing.Color.IndianRed;
            this.lbYTYPstatus.Name = "lbYTYPstatus";
            // 
            // lbYmap
            // 
            resources.ApplyResources(this.lbYmap, "lbYmap");
            this.lbYmap.ForeColor = System.Drawing.Color.IndianRed;
            this.lbYmap.Name = "lbYmap";
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.lbYmap);
            this.Controls.Add(this.lbYTYPstatus);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnSplit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnBrowseOutput);
            this.Controls.Add(this.btnBrowseYMAP);
            this.Controls.Add(this.btnBrowseYTYP);
            this.Controls.Add(this.tbOutput);
            this.Controls.Add(this.tbYMAP);
            this.Controls.Add(this.tbYTYP);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox tbYTYP;
        private TextBox tbYMAP;
        private Button btnBrowseYTYP;
        private Button btnBrowseYMAP;
        private Label label1;
        private Label label2;
        private TextBox tbOutput;
        private Button btnBrowseOutput;
        private Label label3;
        private Button btnSplit;
        private Label label4;
        private Label lbYTYPstatus;
        private Label lbYmap;
    }
}