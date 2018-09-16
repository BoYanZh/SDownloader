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
using Newtonsoft.Json;

namespace SDownloader
{
    public partial class Form1 : Form
    {
        [DllImport("kernel32.dll")]
        public static extern Boolean AllocConsole();
        [DllImport("kernel32.dll")]
        public static extern Boolean FreeConsole();

        private GetSpider MySpider;
        private SpiderSettings MySettings;
        private Thread refreshUIThread;
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
            refreshUIThread = new Thread(refreshUI);
            refreshUIThread.IsBackground = true;
            refreshUIThread.Start();
            pathTextBox.Text = System.Windows.Forms.Application.StartupPath + @"\downloads";
            string tmpStr = Properties.Settings.Default.webInfo;
            WebsiteInfo.websiteList = getWebSitesSettings();
            foreach (WebSiteSetting ws in WebsiteInfo.websiteList) {
                siteComboBox.Items.Add(ws.siteName);
            }
            siteComboBox.Text = siteComboBox.Items[0].ToString();
        }

        private List<WebSiteSetting> getWebSitesSettings() {
            List<WebSiteSetting> tmpWebsiteList = new List<WebSiteSetting>();
            /*string[] mySettings = Properties.Settings.Default.webInfo.Split('\n');
            foreach(string stStr in mySettings) {
                if(stStr.IndexOf('\t') > -1) {
                    WebSiteSetting tmpWebsites = new WebSiteSetting();
                    string[] myInfos = stStr.Split('\t');
                    foreach (string infoStr in myInfos) {
                        if (infoStr.StartsWith("siteName=")) {
                            tmpWebsites.siteName = infoStr.Substring("siteName=".Length);
                        }else if (infoStr.StartsWith("domain=")) {
                            tmpWebsites.domain = infoStr.Substring("domain=".Length);
                        } else if (infoStr.StartsWith("imgType=")) {
                            tmpWebsites.imgType = infoStr.Substring("imgType=".Length);
                        } else if (infoStr.StartsWith("imgKeys=")) {
                            tmpWebsites.imgKeys = infoStr.Substring("imgKeys=".Length).Split('|');
                        } else if (infoStr.StartsWith("pageRegex=")) {
                            tmpWebsites.pageRegex = infoStr.Substring("pageRegex=".Length);
                        } else if (infoStr.StartsWith("urlPattern=")) {
                            tmpWebsites.urlPattern = infoStr.Substring("urlPattern=".Length);
                        } else if (infoStr.StartsWith("firstPageUrlPattern=")) {
                            tmpWebsites.firstPageUrlPattern = infoStr.Substring("firstPageUrlPattern=".Length);
                        }
                    }
                    tmpWebsiteList.Add(tmpWebsites);
                }
            }
            tmpWebsiteList.Clear();*/
            var myJsons = JsonConvert.DeserializeObject<List<WebSiteSetting>>(Properties.Settings.Default.myWebsites);
            tmpWebsiteList = myJsons;
            return tmpWebsiteList;
        }

        private void saveCurrentSettings() {
            /*string tmpstr = string.Empty;
            tmpstr += "siteName=" + siteComboBox.Text + '\t';
            tmpstr += "domain=" + urlTextBox.Text + '\t';
            tmpstr += "imgType=" + imgTypeTextBox.Text + '\t';
            tmpstr += "imgKeys=" + imgKeysTextBox.Text + '\t';
            tmpstr += "pageRegex=" + pageRegTextBox.Text + '\t';
            tmpstr += "urlPattern=" + urlPatTextBox.Text + '\t';
            tmpstr += "firstPageUrlPattern=" + _1stUrlTextBox.Text + '\n';
            string[] mySettings = Properties.Settings.Default.webInfo.Split('\n');
            string replaceStr = string.Empty;
            foreach (string stStr in mySettings) {
                if (stStr.StartsWith("siteName=" + siteComboBox.Text)) {
                    replaceStr = stStr;
                }
            }
            if(replaceStr != string.Empty) {
                Properties.Settings.Default.webInfo = Properties.Settings.Default.webInfo.Replace(replaceStr, tmpstr);
            } else {
                Properties.Settings.Default.webInfo += tmpstr;
            }*/
            string jsonData = JsonConvert.SerializeObject(WebsiteInfo.websiteList);
            Properties.Settings.Default.myWebsites = jsonData;
            Properties.Settings.Default.Save();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("{0} [{1}] {2}", DateTimeOffset.Now.ToString("HH:mm:ss"), 0, "Settings Saved!");
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
                    WebsiteInfo.websiteList[WebsiteInfo.websiteList.FindIndex(s => s.siteName == siteComboBox.Text)] = new WebSiteSetting() {
                        siteName = siteComboBox.Text,
                        domain = urlTextBox.Text,
                        imgType = imgTypeTextBox.Text,
                        imgKeys = imgKeysTextBox.Text.Split('|'),
                        pageRegex = pageRegTextBox.Text,
                        urlPattern = urlPatTextBox.Text,
                        firstPageUrlPattern = _1stUrlTextBox.Text,
                    };
                    saveCurrentSettings();
                    NetworkSpeed.init();
                    MySettings = new SpiderSettings();
                    MySettings.TextKeywords.Add("P");
                    MySettings.imgType = imgTypeTextBox.Text;
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
                            //if (mylistBox.Items.Contains(ex.imgInfoResult.picIndex + " " + ex.imgInfoResult.title)) {
                            mylistBox.Items.Remove(ex.imgInfoResult.picIndex + " " + ex.imgInfoResult.title);
                            //}
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
            /*Task clickTask = new Task(() => {
                DelSpider.work();
            }
            );
            clickTask.Start();*/
            //mylistBox.Items.Remove("1");
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
                saveCurrentSettings();
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
            WebSiteSetting ws = WebsiteInfo.websiteList.Find(s => s.siteName == siteComboBox.Text);
            urlTextBox.Text = ws.domain;
            imgTypeTextBox.Text = ws.imgType;
            pageRegTextBox.Text = ws.pageRegex;
            urlPatTextBox.Text = ws.urlPattern;
            _1stUrlTextBox.Text = ws.firstPageUrlPattern;
            imgKeysTextBox.Text = ws.imgKeys != null ? string.Join("|", ws.imgKeys) : string.Empty;
        }

        private void previewButton_Click(object sender, EventArgs e) {
            WebSiteSetting ws = WebsiteInfo.websiteList.Find(s => s.siteName == siteComboBox.Text);
            //System.Diagnostics.Process.Start(ws.websiteConverter.urlConvert(urlTextBox.Text, imgTypeTextBox.Text, 1));
            string myUrl = WebSiteConverter.getUrl(urlPatTextBox.Text, _1stUrlTextBox.Text, urlTextBox.Text, imgTypeTextBox.Text, 1);
            System.Diagnostics.Process.Start(myUrl);
        }

        private void addSiteButton_Click(object sender, EventArgs e) {
            Properties.Settings.Default.customItem++;
            Properties.Settings.Default.Save();
            WebsiteInfo.websiteList.Add(new WebSiteSetting() {
                siteName = "Custom" + Properties.Settings.Default.customItem
            });
            siteComboBox.Items.Add("Custom" + Properties.Settings.Default.customItem);
        }

        private void siteComboBox_Click(object sender, EventArgs e) {
            WebSiteSetting ws = WebsiteInfo.websiteList.Find(s => s.siteName == siteComboBox.Text);
            ws.domain = urlTextBox.Text;
            ws.imgType = imgTypeTextBox.Text;
            ws.pageRegex = pageRegTextBox.Text;
            ws.urlPattern = urlPatTextBox.Text;
            ws.firstPageUrlPattern = _1stUrlTextBox.Text;
            ws.imgKeys = imgKeysTextBox.Text.Split('|');
            saveCurrentSettings();
        }

        private void browseButton_Click(object sender, EventArgs e)
        {

        }
    }
}
