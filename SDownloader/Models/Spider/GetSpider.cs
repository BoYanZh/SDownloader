using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using SDownloader.Event;
using System.Diagnostics;

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
        private long pageIndex;
        private object writelineLocker = new object(), addListLocker = new object(), finishListLocker = new object();
        private Stopwatch watch;
        //private IWebsiteConverter MyWebsiteConverter;
        public long finishImgCount = 0, fetchImgCount = 0;
        public long finishPageCount = 0, fetchPageCount = 0;
        public bool workFinishFlag = false, fetchIndexPageFlag = false, stopWorkFlag = false, hasEnded = false;
        public SpiderSettings Settings { get; private set; }
        public WebSiteSetting ws;
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
            ws = WebsiteInfo.websiteList.Find(s => s.siteName == settings.siteName);//获取WebSites
            //MyWebsiteConverter = ws.websiteConverter;//获取合适Converter
            Settings = settings;
            OnPageFetched = (s, e) => {
                downloadPageImg(e.imgInfoResult);
            };
            OnPageFinished = (s, e) => {
                if (e != null) myWriteLine("Fetch Page Finished:Index[" + e.imgInfoResult.picIndex + "]Title:" + e.imgInfoResult.title, ConsoleColor.Yellow);
                if ((finishImgCount == fetchImgCount && fetchIndexPageFlag) || workFinishFlag || stopWorkFlag) {
                    end();
                }
            };
        }
        public void run() {
            watch = new Stopwatch();
            Task runTask = new Task(() => {
                fetchContentPage();
            });
            runTask.Start();
        }
        private void end() {
            if (hasEnded) { return; }
            hasEnded = true;
            workFinishFlag = true;
            watch.Stop();
            myWriteLine("All Tasks Finished!", ConsoleColor.Yellow);
            TimeSpan ts = watch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            myWriteLine("Total Time:" + elapsedTime, ConsoleColor.Yellow);
            myWriteLine("Total Images Count:" + finishImgCount + " Total Pages Count:" + finishPageCount, ConsoleColor.Yellow);
            myWriteLine("Total Download Size:" + NetworkSpeed.getTotalSizeText(), ConsoleColor.Yellow);
        }
        /// <summary>
        /// 从索引页获取内容页链接
        /// </summary>
        private void fetchContentPage() {
            myWriteLine("Task:Fetch All Pages Start", ConsoleColor.Yellow);
            watch.Start();
            long invalidPageCount = 0;
            pageIndex = startPage;
            bool isLastPageInvalid = false;
            for (; pageIndex <= endPage & !stopWorkFlag; pageIndex++) {
                myWriteLine("Fetch Index Page Started[" + pageIndex + "]", ConsoleColor.Yellow);
                string[] myContentPageUrl = fetchContentPageUrl(domain, imgType, pageIndex);   //获取索引页面所有链接
                if (myContentPageUrl.Length > 0) {                                             //检测空页
                    myWriteLine("Fetch Index Page Successful[" + pageIndex + "]");
                    isLastPageInvalid = false;
                    fetchImgUrlFromContentPage(myContentPageUrl);
                } else {
                    invalidPageCount = isLastPageInvalid ? ++invalidPageCount : 1;    //计算连续空页
                    isLastPageInvalid = true;
                    myWriteLine("Fetch Index Page Invalid[" + pageIndex + "]", ConsoleColor.Red);
                    if (invalidPageCount >= MAX_INVALID_PAGE_COUNT) {                 //空页过多退出
                        stopWorkFlag = true;
                    }
                }
            }
            fetchIndexPageFlag = true;
            OnPageFinished(null, null);
            myWriteLine("Task:Fetch All Index Pages Finish", ConsoleColor.Yellow);
        }
        private string[] fetchContentPageUrl(string domain, string imgType, long pageIndex) {
            //string myUrl = MyWebsiteConverter.urlConvert(domain, imgType, pageIndex); //得出url
            string myUrl = WebSiteConverter.getUrl(ws.urlPattern, ws.firstPageUrlPattern, ws.domain, ws.imgType, pageIndex);
            string myHtml = MyHttp.getHtml(myUrl, out cookies);                   //获取html
            //string[] myContentPageUrl = MyWebsiteConverter.pageReg(myHtml);                      //获取页面链接
            string[] myContentPageUrl = WebSiteConverter.pageReg(myHtml, ws.pageRegex);
            return myContentPageUrl;
        }
        public void test() {
            //parseLinks(new Uri("https://www.ttt866.com/htm/piclist4/"));
            fetchImgFromContentPage("http://www.dazhuazhi.com/nr/1-1-63/10014640.html", "10014640");
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
        private void fetchImgUrlFromContentPage(string[] page) {
            foreach (string pageUrl in page) {
                do {
                    System.Threading.Thread.Sleep(1);
                } while (fetchPageCount - finishPageCount >= MAX_WAIT_COUNT && !stopWorkFlag);
                if (stopWorkFlag) { break; }//检测退出
                string myPageUrl = pageUrl.IndexOf(@"://") != -1 ? pageUrl : domain + pageUrl;//得出完整url
                string picIndex = pageUrl.Substring(pageUrl.LastIndexOf("/") + 1);//得出图片页面号
                if (picIndex.LastIndexOf(".") != -1) {
                    picIndex = picIndex.Substring(0, picIndex.LastIndexOf("."));
                } else {
                    picIndex = MyHttp.get16bitMd5Str(pageUrl);
                }
                if (JUMP_REPEATED_PAGE == true && allDirectiories.IndexOf(@"\" + picIndex + "-") != -1) {//跳过重复项
                    myWriteLine("Fetch Page Jumped:Index[" + picIndex + "]", ConsoleColor.Red);
                } else {
                    fetchImgFromContentPage(myPageUrl, picIndex);
                }
            }
        }
        private void fetchImgFromContentPage(string pageUrl, string picIndex) {
            string cookies = string.Empty;
            string myPageHtml = MyHttp.getHtml(pageUrl, out cookies);//获取图片url列表
            //string[] imgUrl = MyWebsiteConverter.imgReg(myPageHtml);//获取图片url
            string[] imgUrl = WebSiteConverter.imgReg(myPageHtml, ws.imgKeys);
            for (int i = 0; i < imgUrl.Length; i++) {
                if (imgUrl[i].StartsWith("//")) {
                    imgUrl[i] = "http:" + imgUrl[i];
                } else if (!imgUrl[i].StartsWith("http")) {
                    imgUrl[i] = domain + imgUrl[i];
                }
            }
            string myTitle = MyHttp.getValidFileName(MyHttp.getHtmlTitle(myPageHtml));//获取合法标题
            if (imgUrl.Length > 0) {
                myWriteLine("Fetch Page Successful:Index[" + picIndex + "]Title:" + myTitle);
                lock (addListLocker) {
                    ImgInfo tmpImgInfo = new ImgInfo(imgUrl, myTitle, picIndex, new MyHttp.httpParameter(cookies, "", pageUrl));
                    fetchPageCount++;//计算获取页面数
                    fetchImgCount += imgUrl.Length;//计算获取图片数
                    OnPageFetched(null, new OnFetchedEventArgs(tmpImgInfo));
                }
            } else {
                myWriteLine("Fetch Page Invalid:Index[" + picIndex + "]Title:" + myTitle, ConsoleColor.Red);
            }
        }

        private void downloadPageImg(ImgInfo myImgInfo) {
            if (myImgInfo.imgUrl == null || myImgInfo.imgUrl.Length == 0) return;
            //MyHttpParameter.host = "i1.1100lu.xyz";
            int downloadCount = 0;
            myWriteLine("Download Task Created:" + myImgInfo.picIndex, ConsoleColor.Yellow);
            string myImgPath = savePath + myImgInfo.picIndex + "-" + myImgInfo.title + @"\";//合法目录
            if (!Directory.Exists(myImgPath)) Directory.CreateDirectory(myImgPath);//创建文件目录
            Parallel.For(0, myImgInfo.imgUrl.Length, async (i, state) => {
                myImgInfo.imgUrl[i] = myImgInfo.imgUrl[i].IndexOf("http") != -1 ? myImgInfo.imgUrl[i] : "https:" + myImgInfo.imgUrl[i];//url纠正
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
            );
        }
        private void myWriteLine(string writeStr, ConsoleColor color = ConsoleColor.Green) {
            lock (writelineLocker) {
                Console.ForegroundColor = color;
                Console.WriteLine("{0} [{1}] {2}", DateTimeOffset.Now.ToString("HH:mm:ss"), pageIndex, writeStr);
            }
        }
    }
}
