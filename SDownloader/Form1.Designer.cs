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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.HSButton = new System.Windows.Forms.Button();
            this.DSButton = new System.Windows.Forms.Button();
            this.mylistBox = new System.Windows.Forms.ListBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.imgTypeTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.settingGroupBox = new System.Windows.Forms.GroupBox();
            this.siteComboBox = new System.Windows.Forms.ComboBox();
            this.addSiteButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.browseButton = new System.Windows.Forms.Button();
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.urlTextBox = new System.Windows.Forms.TextBox();
            this.lable5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.endPageNUD = new System.Windows.Forms.NumericUpDown();
            this.startPageNUD = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.previewButton = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.imgKeysTextBox = new System.Windows.Forms.TextBox();
            this._1stUrlTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.urlPatTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pageRegTextBox = new System.Windows.Forms.TextBox();
            this.settingGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.endPageNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.startPageNUD)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // HSButton
            // 
            this.HSButton.Location = new System.Drawing.Point(16, 15);
            this.HSButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.HSButton.Name = "HSButton";
            this.HSButton.Size = new System.Drawing.Size(400, 29);
            this.HSButton.TabIndex = 0;
            this.HSButton.Text = "Start Download";
            this.HSButton.UseVisualStyleBackColor = true;
            this.HSButton.Click += new System.EventHandler(this.HSButton_Click);
            // 
            // DSButton
            // 
            this.DSButton.Location = new System.Drawing.Point(431, 15);
            this.DSButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DSButton.Name = "DSButton";
            this.DSButton.Size = new System.Drawing.Size(400, 29);
            this.DSButton.TabIndex = 1;
            this.DSButton.TabStop = false;
            this.DSButton.Text = "Clean Up Files";
            this.DSButton.UseVisualStyleBackColor = true;
            this.DSButton.Click += new System.EventHandler(this.DSButton_Click);
            // 
            // mylistBox
            // 
            this.mylistBox.FormattingEnabled = true;
            this.mylistBox.ItemHeight = 15;
            this.mylistBox.Location = new System.Drawing.Point(15, 329);
            this.mylistBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mylistBox.Name = "mylistBox";
            this.mylistBox.Size = new System.Drawing.Size(813, 229);
            this.mylistBox.TabIndex = 8;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(13, 566);
            this.progressBar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(815, 29);
            this.progressBar.TabIndex = 4;
            // 
            // imgTypeTextBox
            // 
            this.imgTypeTextBox.Location = new System.Drawing.Point(463, 56);
            this.imgTypeTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 0, 4);
            this.imgTypeTextBox.Name = "imgTypeTextBox";
            this.imgTypeTextBox.Size = new System.Drawing.Size(343, 25);
            this.imgTypeTextBox.TabIndex = 5;
            this.imgTypeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.imgTypeTextBox.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(416, 60);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "Type";
            // 
            // settingGroupBox
            // 
            this.settingGroupBox.Controls.Add(this.siteComboBox);
            this.settingGroupBox.Controls.Add(this.addSiteButton);
            this.settingGroupBox.Controls.Add(this.label2);
            this.settingGroupBox.Controls.Add(this.browseButton);
            this.settingGroupBox.Controls.Add(this.pathTextBox);
            this.settingGroupBox.Controls.Add(this.urlTextBox);
            this.settingGroupBox.Controls.Add(this.lable5);
            this.settingGroupBox.Controls.Add(this.label3);
            this.settingGroupBox.Controls.Add(this.endPageNUD);
            this.settingGroupBox.Controls.Add(this.startPageNUD);
            this.settingGroupBox.Controls.Add(this.label4);
            this.settingGroupBox.Controls.Add(this.label1);
            this.settingGroupBox.Controls.Add(this.imgTypeTextBox);
            this.settingGroupBox.Location = new System.Drawing.Point(15, 51);
            this.settingGroupBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.settingGroupBox.Name = "settingGroupBox";
            this.settingGroupBox.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.settingGroupBox.Size = new System.Drawing.Size(813, 128);
            this.settingGroupBox.TabIndex = 2;
            this.settingGroupBox.TabStop = false;
            this.settingGroupBox.Text = "Settings";
            // 
            // siteComboBox
            // 
            this.siteComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.siteComboBox.FormattingEnabled = true;
            this.siteComboBox.Location = new System.Drawing.Point(57, 24);
            this.siteComboBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.siteComboBox.Name = "siteComboBox";
            this.siteComboBox.Size = new System.Drawing.Size(319, 23);
            this.siteComboBox.TabIndex = 4;
            this.siteComboBox.SelectedIndexChanged += new System.EventHandler(this.siteComboBox_SelectedIndexChanged);
            this.siteComboBox.Click += new System.EventHandler(this.siteComboBox_Click);
            // 
            // addSiteButton
            // 
            this.addSiteButton.Location = new System.Drawing.Point(376, 22);
            this.addSiteButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.addSiteButton.Name = "addSiteButton";
            this.addSiteButton.Size = new System.Drawing.Size(25, 28);
            this.addSiteButton.TabIndex = 34;
            this.addSiteButton.Text = "+";
            this.addSiteButton.UseVisualStyleBackColor = true;
            this.addSiteButton.Click += new System.EventHandler(this.addSiteButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 84);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 30);
            this.label2.TabIndex = 33;
            this.label2.Text = "Save\r\nPath";
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(703, 90);
            this.browseButton.Margin = new System.Windows.Forms.Padding(0, 4, 4, 4);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(104, 29);
            this.browseButton.TabIndex = 31;
            this.browseButton.Text = "Browse...";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // pathTextBox
            // 
            this.pathTextBox.Location = new System.Drawing.Point(57, 90);
            this.pathTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 0, 4);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.ReadOnly = true;
            this.pathTextBox.Size = new System.Drawing.Size(644, 25);
            this.pathTextBox.TabIndex = 32;
            this.pathTextBox.TabStop = false;
            this.pathTextBox.WordWrap = false;
            // 
            // urlTextBox
            // 
            this.urlTextBox.Location = new System.Drawing.Point(57, 56);
            this.urlTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 0, 4);
            this.urlTextBox.Name = "urlTextBox";
            this.urlTextBox.Size = new System.Drawing.Size(343, 25);
            this.urlTextBox.TabIndex = 29;
            this.urlTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.urlTextBox.WordWrap = false;
            // 
            // lable5
            // 
            this.lable5.AutoSize = true;
            this.lable5.Location = new System.Drawing.Point(1, 60);
            this.lable5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lable5.Name = "lable5";
            this.lable5.Size = new System.Drawing.Size(55, 15);
            this.lable5.TabIndex = 30;
            this.lable5.Text = "Domain";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(413, 26);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 15);
            this.label3.TabIndex = 28;
            this.label3.Text = "Range";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // endPageNUD
            // 
            this.endPageNUD.Location = new System.Drawing.Point(640, 22);
            this.endPageNUD.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.endPageNUD.Name = "endPageNUD";
            this.endPageNUD.Size = new System.Drawing.Size(167, 25);
            this.endPageNUD.TabIndex = 27;
            this.endPageNUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // startPageNUD
            // 
            this.startPageNUD.Location = new System.Drawing.Point(463, 22);
            this.startPageNUD.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.startPageNUD.Name = "startPageNUD";
            this.startPageNUD.Size = new System.Drawing.Size(167, 25);
            this.startPageNUD.TabIndex = 26;
            this.startPageNUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 28);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 15);
            this.label4.TabIndex = 17;
            this.label4.Text = "Site";
            // 
            // previewButton
            // 
            this.previewButton.Location = new System.Drawing.Point(431, 15);
            this.previewButton.Margin = new System.Windows.Forms.Padding(0, 4, 4, 4);
            this.previewButton.Name = "previewButton";
            this.previewButton.Size = new System.Drawing.Size(399, 29);
            this.previewButton.TabIndex = 1;
            this.previewButton.Text = "Preview WebSite";
            this.previewButton.UseVisualStyleBackColor = true;
            this.previewButton.Click += new System.EventHandler(this.previewButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel4});
            this.statusStrip1.Location = new System.Drawing.Point(0, 606);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(843, 26);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 18;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.AutoSize = false;
            this.toolStripStatusLabel1.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(385, 21);
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
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.imgKeysTextBox);
            this.groupBox1.Controls.Add(this._1stUrlTextBox);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.urlPatTextBox);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.pageRegTextBox);
            this.groupBox1.Location = new System.Drawing.Point(16, 182);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(813, 130);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Advance";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 88);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 30);
            this.label8.TabIndex = 41;
            this.label8.Text = "Img\r\nKeys";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // imgKeysTextBox
            // 
            this.imgKeysTextBox.Location = new System.Drawing.Point(56, 91);
            this.imgKeysTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 0, 4);
            this.imgKeysTextBox.Name = "imgKeysTextBox";
            this.imgKeysTextBox.Size = new System.Drawing.Size(748, 25);
            this.imgKeysTextBox.TabIndex = 40;
            this.imgKeysTextBox.WordWrap = false;
            // 
            // _1stUrlTextBox
            // 
            this._1stUrlTextBox.Location = new System.Drawing.Point(461, 58);
            this._1stUrlTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 0, 4);
            this._1stUrlTextBox.Name = "_1stUrlTextBox";
            this._1stUrlTextBox.Size = new System.Drawing.Size(343, 25);
            this._1stUrlTextBox.TabIndex = 39;
            this._1stUrlTextBox.WordWrap = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(419, 54);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 30);
            this.label7.TabIndex = 38;
            this.label7.Text = "1st\r\nUrl";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // urlPatTextBox
            // 
            this.urlPatTextBox.Location = new System.Drawing.Point(56, 58);
            this.urlPatTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 0, 4);
            this.urlPatTextBox.Name = "urlPatTextBox";
            this.urlPatTextBox.Size = new System.Drawing.Size(343, 25);
            this.urlPatTextBox.TabIndex = 37;
            this.urlPatTextBox.WordWrap = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 56);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 30);
            this.label6.TabIndex = 36;
            this.label6.Text = "Url\r\nPat";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 20);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 30);
            this.label5.TabIndex = 35;
            this.label5.Text = "Page\r\nReg";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pageRegTextBox
            // 
            this.pageRegTextBox.Location = new System.Drawing.Point(56, 24);
            this.pageRegTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 0, 4);
            this.pageRegTextBox.Name = "pageRegTextBox";
            this.pageRegTextBox.Size = new System.Drawing.Size(748, 25);
            this.pageRegTextBox.TabIndex = 34;
            this.pageRegTextBox.WordWrap = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 632);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.settingGroupBox);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.mylistBox);
            this.Controls.Add(this.HSButton);
            this.Controls.Add(this.previewButton);
            this.Controls.Add(this.DSButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Strange Downloader";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.settingGroupBox.ResumeLayout(false);
            this.settingGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.endPageNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.startPageNUD)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.TextBox imgTypeTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox settingGroupBox;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ComboBox siteComboBox;
        private System.Windows.Forms.Button previewButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown endPageNUD;
        private System.Windows.Forms.NumericUpDown startPageNUD;
        private System.Windows.Forms.TextBox urlTextBox;
        private System.Windows.Forms.Label lable5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox imgKeysTextBox;
        private System.Windows.Forms.TextBox _1stUrlTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox urlPatTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox pageRegTextBox;
        private System.Windows.Forms.Button addSiteButton;
    }
}