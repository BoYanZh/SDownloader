using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

namespace SDownloader
{
    public partial class Form1 : Form
    {
        [DllImport("kernel32.dll")]
        public static extern Boolean AllocConsole();
        [DllImport("kernel32.dll")]
        public static extern Boolean FreeConsole();
        public List<string> urlList;
        public GetSpider MySpider;
        public SpiderSettings MySettings;
        public Thread refreshUIThread;
        public Form1() {
            InitializeComponent();
            AllocConsole();
            CheckForIllegalCrossThreadCalls = false;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Copyright 2017 BY. All rights reserved.\n");
            startPageNUD.Maximum = long.MaxValue;
            startPageNUD.Value = 1;
            endPageNUD.Maximum = long.MaxValue;
            endPageNUD.Value = 2500;
            siteComboBox.Text = siteComboBox.Items[0].ToString();
            refreshUIThread = new Thread(refreshUI);
            refreshUIThread.IsBackground = true;
            refreshUIThread.Start();
            pathTextBox.Text = System.Windows.Forms.Application.StartupPath + @"\downloads";
        }

        private void refreshUI() {
            while (true) {
                toolStripStatusLabel1.Text = DateTime.Now.ToString();
                if (MySpider != null) {
                    toolStripStatusLabel2.Text = "Downloads:" + MySpider.finishImgCount.ToString();
                    toolStripStatusLabel3.Text = MySpider.finishPageCount + "/" + MySpider.fetchPageCount;
                    toolStripStatusLabel4.Text = NetworkSpeed.getSpeed();
                }
                Thread.Sleep(500);
            }
        }

        private void HSButton_Click(object sender, EventArgs e) {
            if (HSButton.Text == "Start Download") {
                Task clickTask = new Task(() => {
                    HSButton.Text = "Finish Download";
                    settingGroupBox.Enabled = false;
                    NetworkSpeed.init();
                    MySettings = new SpiderSettings();
                    MySettings.TextKeywords.Add("P");
                    MySettings.imgType = picTypeTextBox.Text;
                    MySettings.domain = urlTextBox.Text;
                    MySettings.siteName = siteComboBox.Text;
                    MySettings.savePath = pathTextBox.Text;
                    MySettings.startPage = (long)startPageNUD.Value;
                    MySettings.endPage = (long)endPageNUD.Value;
                    MySpider = new GetSpider(MySettings);
                    MySpider.OnPageFetched += (s, ex) => {
                        mylistBox.Items.Insert(0, ex.imgInfoResult.picIndex + " " + ex.imgInfoResult.title);
                        progressBar.Maximum = (int)MySpider.fetchPageCount;
                    };
                    MySpider.OnPageFinished += (s, ex) => {
                        if (ex != null && MySpider != null) {
                            if (mylistBox.Items.Contains(ex.imgInfoResult.picIndex + " " + ex.imgInfoResult.title)) {
                                mylistBox.Items.Remove(ex.imgInfoResult.picIndex + " " + ex.imgInfoResult.title);
                            }
                            progressBar.Value = (int)MySpider.finishPageCount;
                        }
                        if (MySpider != null && MySpider.workFinishFlag) {
                            progressBar.Value = 0;
                            mylistBox.Items.Clear();
                            settingGroupBox.Enabled = true;
                            HSButton.Enabled = true;
                            HSButton.Text = "Start Download";
                        }
                    };
                    MySpider.run();
                    //MySpider.test();
                }
                );
                clickTask.Start();
            } else if (HSButton.Text == "Finish Download") {
                HSButton.Enabled = false;
                MySpider.stopWorkFlag = true;
            }
        }

        private void DSButton_Click(object sender, EventArgs e) {
            Task clickTask = new Task(() => {
                DelSpider.work();
            }
            );
            clickTask.Start();
        }

        private void browserButton_Click(object sender, EventArgs e) {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog1.Description = "Select Save Path:";
            folderBrowserDialog1.SelectedPath = pathTextBox.Text;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                pathTextBox.Text = folderBrowserDialog1.SelectedPath + @"\";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            DialogResult result = MessageBox.Show("Sure to Exit？", "Tip", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK) {
                e.Cancel = false;  //点击OK   
            } else {
                e.Cancel = true;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e) {
            FreeConsole();
            Application.Exit();
        }

        private void siteComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            switch (siteComboBox.Text) {
                case "猫咪AV":
                    urlTextBox.Text = "https://www.ttt844.com/";
                    picTypeTextBox.Text = "piclist3";
                    break;
                case "千百撸":
                    urlTextBox.Text = "https://9999av.co/";
                    picTypeTextBox.Text = "oumei";
                    break;
                case "色哥哥":
                    urlTextBox.Text = "http://48td.com/";
                    picTypeTextBox.Text = "13";
                    break;
                case "五月香":
                    urlTextBox.Text = "http://www.dazhuazhi.com/";
                    picTypeTextBox.Text = "68";
                    break;
                case "2017MN":
                    urlTextBox.Text = "http://www.2017mn.com/";
                    picTypeTextBox.Text = "oumei";
                    break;
                case "桃花族":
                    urlTextBox.Text = "http://thibt.com/";
                    picTypeTextBox.Text = "221";
                    break;
            }
        }

        private void previewButton_Click(object sender, EventArgs e) {
            IWebSiteInfo MyWebSiteInfo;
            switch (siteComboBox.Text) {
                case "猫咪AV":
                    MyWebSiteInfo = new MaomiAV();
                    System.Diagnostics.Process.Start(MyWebSiteInfo.urlConvert(urlTextBox.Text, picTypeTextBox.Text, 1));
                    break;
                case "千百撸":
                    MyWebSiteInfo = new QianBaiLu();
                    System.Diagnostics.Process.Start(MyWebSiteInfo.urlConvert(urlTextBox.Text, picTypeTextBox.Text, 1));
                    break;
                case "色哥哥":
                    MyWebSiteInfo = new SeGeGe();
                    System.Diagnostics.Process.Start(MyWebSiteInfo.urlConvert(urlTextBox.Text, picTypeTextBox.Text, 1));
                    break;
                case "五月香":
                    MyWebSiteInfo = new WuYueXiang();
                    System.Diagnostics.Process.Start(MyWebSiteInfo.urlConvert(urlTextBox.Text, picTypeTextBox.Text, 1));
                    break;
                case "2017MN":
                    MyWebSiteInfo = new _2017MN();
                    System.Diagnostics.Process.Start(MyWebSiteInfo.urlConvert(urlTextBox.Text, picTypeTextBox.Text, 1));
                    break;
                case "桃花族":
                    MyWebSiteInfo = new TaoHuaZu();
                    System.Diagnostics.Process.Start(MyWebSiteInfo.urlConvert(urlTextBox.Text, picTypeTextBox.Text, 1));
                    break;
            }
        }
    }
}
