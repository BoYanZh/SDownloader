namespace SDownloader
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.HSButton = new System.Windows.Forms.Button();
            this.DSButton = new System.Windows.Forms.Button();
            this.mylistBox = new System.Windows.Forms.ListBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.picTypeTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.startPageNUD = new System.Windows.Forms.NumericUpDown();
            this.endPageNUD = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.lable5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.urlTextBox = new System.Windows.Forms.TextBox();
            this.settingGroupBox = new System.Windows.Forms.GroupBox();
            this.siteComboBox = new System.Windows.Forms.ComboBox();
            this.previewButton = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.startPageNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.endPageNUD)).BeginInit();
            this.settingGroupBox.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // HSButton
            // 
            this.HSButton.Location = new System.Drawing.Point(12, 12);
            this.HSButton.Name = "HSButton";
            this.HSButton.Size = new System.Drawing.Size(300, 23);
            this.HSButton.TabIndex = 0;
            this.HSButton.Text = "Start Download";
            this.HSButton.UseVisualStyleBackColor = true;
            this.HSButton.Click += new System.EventHandler(this.HSButton_Click);
            // 
            // DSButton
            // 
            this.DSButton.Enabled = false;
            this.DSButton.Location = new System.Drawing.Point(323, 12);
            this.DSButton.Name = "DSButton";
            this.DSButton.Size = new System.Drawing.Size(300, 23);
            this.DSButton.TabIndex = 1;
            this.DSButton.TabStop = false;
            this.DSButton.Text = "Clean Up Files";
            this.DSButton.UseVisualStyleBackColor = true;
            this.DSButton.Click += new System.EventHandler(this.DSButton_Click);
            // 
            // mylistBox
            // 
            this.mylistBox.FormattingEnabled = true;
            this.mylistBox.ItemHeight = 12;
            this.mylistBox.Location = new System.Drawing.Point(11, 155);
            this.mylistBox.Name = "mylistBox";
            this.mylistBox.Size = new System.Drawing.Size(611, 256);
            this.mylistBox.TabIndex = 8;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(10, 417);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(611, 23);
            this.progressBar.TabIndex = 4;
            // 
            // pathTextBox
            // 
            this.pathTextBox.Location = new System.Drawing.Point(67, 20);
            this.pathTextBox.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.ReadOnly = true;
            this.pathTextBox.Size = new System.Drawing.Size(460, 21);
            this.pathTextBox.TabIndex = 2;
            this.pathTextBox.TabStop = false;
            this.pathTextBox.WordWrap = false;
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(527, 19);
            this.browseButton.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(78, 23);
            this.browseButton.TabIndex = 2;
            this.browseButton.Text = "Browse...";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browserButton_Click);
            // 
            // picTypeTextBox
            // 
            this.picTypeTextBox.Location = new System.Drawing.Point(67, 74);
            this.picTypeTextBox.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.picTypeTextBox.Name = "picTypeTextBox";
            this.picTypeTextBox.Size = new System.Drawing.Size(240, 21);
            this.picTypeTextBox.TabIndex = 5;
            this.picTypeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.picTypeTextBox.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "Type";
            // 
            // startPageNUD
            // 
            this.startPageNUD.Location = new System.Drawing.Point(368, 74);
            this.startPageNUD.Name = "startPageNUD";
            this.startPageNUD.Size = new System.Drawing.Size(115, 21);
            this.startPageNUD.TabIndex = 6;
            this.startPageNUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // endPageNUD
            // 
            this.endPageNUD.Location = new System.Drawing.Point(490, 74);
            this.endPageNUD.Name = "endPageNUD";
            this.endPageNUD.Size = new System.Drawing.Size(115, 21);
            this.endPageNUD.TabIndex = 7;
            this.endPageNUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(327, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "Range";
            // 
            // lable5
            // 
            this.lable5.AutoSize = true;
            this.lable5.Location = new System.Drawing.Point(27, 50);
            this.lable5.Name = "lable5";
            this.lable5.Size = new System.Drawing.Size(23, 12);
            this.lable5.TabIndex = 14;
            this.lable5.Text = "URL";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 16;
            this.label2.Text = "Save Path";
            // 
            // urlTextBox
            // 
            this.urlTextBox.Location = new System.Drawing.Point(67, 47);
            this.urlTextBox.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.urlTextBox.Name = "urlTextBox";
            this.urlTextBox.Size = new System.Drawing.Size(416, 21);
            this.urlTextBox.TabIndex = 3;
            this.urlTextBox.WordWrap = false;
            // 
            // settingGroupBox
            // 
            this.settingGroupBox.Controls.Add(this.siteComboBox);
            this.settingGroupBox.Controls.Add(this.label2);
            this.settingGroupBox.Controls.Add(this.urlTextBox);
            this.settingGroupBox.Controls.Add(this.lable5);
            this.settingGroupBox.Controls.Add(this.label3);
            this.settingGroupBox.Controls.Add(this.endPageNUD);
            this.settingGroupBox.Controls.Add(this.startPageNUD);
            this.settingGroupBox.Controls.Add(this.label1);
            this.settingGroupBox.Controls.Add(this.picTypeTextBox);
            this.settingGroupBox.Controls.Add(this.browseButton);
            this.settingGroupBox.Controls.Add(this.pathTextBox);
            this.settingGroupBox.Location = new System.Drawing.Point(11, 41);
            this.settingGroupBox.Name = "settingGroupBox";
            this.settingGroupBox.Size = new System.Drawing.Size(610, 108);
            this.settingGroupBox.TabIndex = 2;
            this.settingGroupBox.TabStop = false;
            this.settingGroupBox.Text = "Setting";
            // 
            // siteComboBox
            // 
            this.siteComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.siteComboBox.FormattingEnabled = true;
            this.siteComboBox.Items.AddRange(new object[] {
            "猫咪AV",
            "千百撸",
            "2017MN",
            "色哥哥",
            "五月香",
            "桃花族"});
            this.siteComboBox.Location = new System.Drawing.Point(490, 48);
            this.siteComboBox.Name = "siteComboBox";
            this.siteComboBox.Size = new System.Drawing.Size(115, 20);
            this.siteComboBox.TabIndex = 4;
            this.siteComboBox.SelectedIndexChanged += new System.EventHandler(this.siteComboBox_SelectedIndexChanged);
            // 
            // previewButton
            // 
            this.previewButton.Location = new System.Drawing.Point(323, 12);
            this.previewButton.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.previewButton.Name = "previewButton";
            this.previewButton.Size = new System.Drawing.Size(300, 23);
            this.previewButton.TabIndex = 1;
            this.previewButton.Text = "Preview WebSite";
            this.previewButton.UseVisualStyleBackColor = true;
            this.previewButton.Click += new System.EventHandler(this.previewButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel4});
            this.statusStrip1.Location = new System.Drawing.Point(0, 443);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(632, 26);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 18;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.AutoSize = false;
            this.toolStripStatusLabel1.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(179, 21);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.Text = "1970/1/1 00:00:00";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.AutoSize = false;
            this.toolStripStatusLabel2.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(146, 21);
            this.toolStripStatusLabel2.Text = "Downloads:0";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.AutoSize = false;
            this.toolStripStatusLabel3.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(146, 21);
            this.toolStripStatusLabel3.Text = "0/0";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.AutoSize = false;
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(146, 21);
            this.toolStripStatusLabel4.Text = "0 B/s";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 469);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.settingGroupBox);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.mylistBox);
            this.Controls.Add(this.HSButton);
            this.Controls.Add(this.previewButton);
            this.Controls.Add(this.DSButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Strange Downloader";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.startPageNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.endPageNUD)).EndInit();
            this.settingGroupBox.ResumeLayout(false);
            this.settingGroupBox.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void Form1_FormClosing1(object sender, System.Windows.Forms.FormClosingEventArgs e) {
            throw new System.NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.Button HSButton;
        private System.Windows.Forms.Button DSButton;
        public System.Windows.Forms.ListBox mylistBox;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.TextBox picTypeTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown startPageNUD;
        private System.Windows.Forms.NumericUpDown endPageNUD;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lable5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox urlTextBox;
        private System.Windows.Forms.GroupBox settingGroupBox;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ComboBox siteComboBox;
        private System.Windows.Forms.Button previewButton;
    }
}