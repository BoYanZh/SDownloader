using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using SDownloader.Event;

namespace SDownloader
{
    public class GetSpider
    {
        private const int MAX_WAIT_COUNT = 20;             //最大等待页面
        private const int MAX_INVALID_PAGE_COUNT = 5;      //最大空页数量
        private const bool JUMP_REPEATED_PAGE = true;      //是否跳过重复
        private string domain;                             //域名
        private string siteName;                           //网站名称
        private long startPage, endPage;                   //任务始末
        private string imgType;                            //图片类型
        private string savePath, allDirectiories;          //保存路径
        private string cookies = string.Empty;
        private object writelineLocker = new object(), addListLocker = new object(), finishListLocker = new object();
        private IWebSiteInfo MyWebSiteInfo;
        public long finishImgCount = 0, fetchImgCount = 0;
        public long finishPageCount = 0, fetchPageCount = 0;
        public bool workFinishFlag = false, fetchIndexPageFlag = false;
        public SpiderSettings Settings { get; private set; }
        public event EventHandler<OnFetchedEventArgs> OnPageFetched;
        public event EventHandler<OnFinishedEventArgs> OnPageFinished;
        public struct ImgInfo
        {
            public string[] imgUrl;
            public string title;
            public string picIndex;
            public MyHttp.httpParameter HP;
            public ImgInfo(string[] myImgUrl, string myTitle, string myPicIndex, MyHttp.httpParameter myHP) {
                imgUrl = myImgUrl;
                title = myTitle;
                picIndex = myPicIndex;
                HP = myHP;
            }
        }
        public GetSpider(SpiderSettings settings) {
            imgType = settings.imgType;
            domain = settings.domain;
            siteName = settings.siteName;
            savePath = settings.savePath + @"\" + siteName + @"\" + imgType + @"\";//合法目录
            if (!Directory.Exists(savePath)) Directory.CreateDirectory(savePath);//创建目录
            allDirectiories = String.Join("|", Directory.GetDirectories(savePath, "*"));//获取所有文件名
            startPage = settings.startPage; endPage = settings.endPage;
            switch (settings.siteName) {
                case "猫咪AV":
                    MyWebSiteInfo = new MaomiAV();
                    break;
                case "千百撸":
                    MyWebSiteInfo = new QianBaiLu();
                    break;
                case "色哥哥":
                    MyWebSiteInfo = new SeGeGe();
                    break;
                case "五月香":
                    MyWebSiteInfo = new WuYueXiang();
                    break;
                case "2017MN":
                    MyWebSiteInfo = new _2017MN();
                    break;
                case "桃花族":
                    MyWebSiteInfo = new TaoHuaZu();
                    break;
            }
            Settings = settings;
            OnPageFetched = (s, e) => {
                downloadPageImg(e.imgInfoResult);
            };
            OnPageFinished = (s, e) => {
                if (e != null) myWriteLine("Fetch Page Finished:Index[" + e.imgInfoResult.picIndex + "]Title:" + e.imgInfoResult.title, ConsoleColor.Yellow);
                if (finishImgCount == fetchImgCount && fetchIndexPageFlag) {
                    workFinishFlag = true;
                    myWriteLine("All Tasks Finished!", ConsoleColor.Yellow);
                }
            };
        }
        public void run() {
            Task getAllPagesTask = new Task(() => {
                fetchAllPages();
            });
            getAllPagesTask.Start();
        }
        /// <summary>
        /// 获取所有可下载图片链接
        /// </summary>
        private void fetchAllPages() {
            myWriteLine("Task:Fetch All Pages Start", ConsoleColor.Yellow);
            long invalidPageCount = 0, pageIndex = startPage;
            for (; pageIndex <= endPage & !workFinishFlag; pageIndex++) {
                string myUrl = MyWebSiteInfo.urlConvert(domain, imgType, pageIndex); //得出url
                myWriteLine("Fetch Index Page Started[" + pageIndex + "]");           //
                string myHtml = MyHttp.getHtml(myUrl, out cookies);                   //获取html
                string[] myPage = MyWebSiteInfo.pageReg(myHtml);                      //获取页面链接
                if (myPage.Length <= 0) {                                             //检测空页
                    invalidPageCount++;                                               //
                    myWriteLine("Fetch Index Page Invalid[" + pageIndex + "]", ConsoleColor.Red);
                    if (invalidPageCount >= MAX_INVALID_PAGE_COUNT) {                 //空页过多退出
                        workFinishFlag = true;
                        OnPageFinished(null, null);
                        break;
                    }
                } else {
                    fetchImgUrlFromPage(myPage);
                }
            }
            fetchIndexPageFlag = true;
            myWriteLine("Task:Fetch All Index Pages Finish", ConsoleColor.Yellow);
        }
        public void test() {
            parseLinks(new Uri("https://www.ttt866.com/htm/piclist4/"));
        }
        private void parseLinks(Uri uri) {
            string html = MyHttp.getHtml(uri.AbsoluteUri);
            var urlDictionary = new Dictionary<string, string>();
            Match match = Regex.Match(html, "(?i)<a .*?href=\"([^\"]+)\"[^>]*>(.*?)</a>");
            while (match.Success) {
                // 以 href 作为 key
                string urlKey = match.Groups[1].Value;
                // 以 text 作为 value
                string urlValue = Regex.Replace(match.Groups[2].Value, "(?i)<.*?>", string.Empty);
                urlDictionary[urlKey] = urlValue;
                match = match.NextMatch();
            }
            foreach (var item in urlDictionary) {
                string href = item.Key;
                string text = item.Value;

                if (!string.IsNullOrEmpty(href)) {
                    bool canBeAdd = true;

                    if (Settings.EscapeLinks != null && Settings.EscapeLinks.Count > 0) {
                        if (Settings.EscapeLinks.Any(suffix => href.EndsWith(suffix, StringComparison.OrdinalIgnoreCase))) {
                            canBeAdd = false;
                        }
                    }

                    if (Settings.HrefKeywords != null && Settings.HrefKeywords.Count > 0) {
                        if (!Settings.HrefKeywords.Any(href.Contains)) {
                            canBeAdd = false;
                        }
                    }

                    if (Settings.TextKeywords != null && Settings.TextKeywords.Count > 0) {
                        if (!Settings.TextKeywords.Any(text.Contains)) {
                            canBeAdd = false;
                        }
                    }

                    if (canBeAdd) {
                        string url = href.Replace("%3f", "?")
                            .Replace("%3d", "=")
                            .Replace("%2f", "/")
                            .Replace("&amp;", "&");

                        if (string.IsNullOrEmpty(url) || url.StartsWith("#")
                            || url.StartsWith("mailto:", StringComparison.OrdinalIgnoreCase)
                            || url.StartsWith("javascript:", StringComparison.OrdinalIgnoreCase)) {
                            continue;
                        }
                        var baseUri = uri;
                        Uri currentUri = url.StartsWith("http", StringComparison.OrdinalIgnoreCase)
                                             ? new Uri(url)
                                             : new Uri(baseUri, url);

                        url = currentUri.AbsoluteUri;
                        myWriteLine(url);
                    }
                }
            }
        }
        private void fetchImgUrlFromPage(string[] page) {
            foreach (string pageUrl in page) {
                do {
                } while (fetchPageCount - finishPageCount >= MAX_WAIT_COUNT && !workFinishFlag);
                if (workFinishFlag) { break; }//检测退出
                string myPageUrl = pageUrl.IndexOf(@"://") != -1 ? pageUrl : domain + pageUrl;//得出完整url
                string picIndex = pageUrl.Substring(pageUrl.LastIndexOf("/") + 1);//得出图片页面号
                if (picIndex.LastIndexOf(".") == -1) { continue; }
                picIndex = picIndex.Substring(0, picIndex.LastIndexOf("."));
                if (JUMP_REPEATED_PAGE == true && allDirectiories.IndexOf(@"\" + picIndex + "-") != -1)//跳过重复项
                {
                    myWriteLine("Fetch Page Jumped:Index[" + picIndex + "]", ConsoleColor.Red);
                } else {
                    string myPageHtml = MyHttp.getHtml(myPageUrl, out cookies);//获取图片url列表
                    string[] imgUrl = MyWebSiteInfo.imgReg(myPageHtml);//获取图片url
                    if (imgUrl.Length > 0) {
                        string myTitle = getValidFileName(MyHttp.getHtmlTitle(myPageHtml));//获取合法标题
                        myWriteLine("Fetch Page Successful:Index[" + picIndex + "]Title:" + myTitle);
                        if (!Directory.Exists(savePath + picIndex + "-" + myTitle + @"\")) Directory.CreateDirectory(savePath + picIndex + "-" + myTitle + @"\");//创建文件目录
                        lock (addListLocker) {
                            ImgInfo tmpImgInfo = new ImgInfo(imgUrl, myTitle, picIndex, new MyHttp.httpParameter(cookies, "", myPageUrl));
                            fetchPageCount++;//计算获取页面数
                            fetchImgCount += imgUrl.Length;//计算获取图片数
                            OnPageFetched(null, new OnFetchedEventArgs(tmpImgInfo));
                        }
                    }
                }
            }
        }
        private string getValidFileName(string fileName) {
            StringBuilder rBuilder = new StringBuilder(fileName);
            foreach (char rInvalidChar in Path.GetInvalidFileNameChars())
                rBuilder.Replace(rInvalidChar.ToString(), string.Empty);
            return rBuilder.ToString();
        }
        private void downloadPageImg(ImgInfo myImgInfo) {
            if (myImgInfo.imgUrl == null || myImgInfo.imgUrl.Length == 0) return;
            //MyHttpParameter.host = "i1.1100lu.xyz";
            int downloadCount = 0;
            myWriteLine("Download Task Create:" + myImgInfo.picIndex, ConsoleColor.Yellow);
            string myImgPath = savePath + myImgInfo.picIndex + "-" + myImgInfo.title + @"\";//合法目录
            Parallel.For(0, myImgInfo.imgUrl.Length, async (i, state) => {
                if (myImgInfo.imgUrl[i].IndexOf("pan.baidu.com") == -1) {//跳过无用链接
                    myImgInfo.imgUrl[i] = myImgInfo.imgUrl[i].IndexOf("http") != -1 ? myImgInfo.imgUrl[i] : "https:" + myImgInfo.imgUrl[i];//url纠正
                    //string saveRe = await MyHttp.SaveImageFromWeb(myImgInfo.imgUrl[i], myImgPath, (i + 1) + "-" + myImgInfo.picIndex, myImgInfo.HP);//异步获取图片
                    string saveRe = await MyHttp.getImg(myImgInfo.imgUrl[i], myImgPath, (i + 1) + "-" + myImgInfo.picIndex, myImgInfo.HP);//异步获取图片
                    myWriteLine("Img Downloaded Result:" + saveRe, saveRe.IndexOf("Error:") != -1 ? ConsoleColor.Red : ConsoleColor.Green);//检测是否发生错误
                    finishImgCount++;
                    if (++downloadCount == myImgInfo.imgUrl.Length) {//检测整页完成
                        lock (finishListLocker) {
                            finishPageCount++;
                            OnPageFinished(null, new OnFinishedEventArgs(myImgInfo));
                        }
                    }
                }
            }
            );
        }
        private void myWriteLine(string writeStr, ConsoleColor color = ConsoleColor.Green) {
            lock (writelineLocker) {
                Console.ForegroundColor = color;
                Console.WriteLine("{0} {1}", DateTimeOffset.Now.ToString("HH:mm:ss"), writeStr);
            }
        }
    }
}
