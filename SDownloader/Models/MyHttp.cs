using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using SufeiUtil;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.IO.Compression;
using System.Drawing;

namespace SDownloader
{
    public class MyHttp
    {
        const int HTML_TIMEOUT = 10000;
        const int IMG_TIMEOUT = 20000;
        const int MIN_IMG_SIZE = 5000;
        static CookieContainer CookiesContainer { get; set; }//定义Cookie容器
        static string cookieString { get; set; }
        public struct httpParameter
        {
            public string cookie;
            public string host;
            public string referer;
            public httpParameter(string myCookie, string myHost, string myReferer) {
                cookie = myCookie;
                host = myHost;
                referer = myReferer;
            }
        }
        public static string getHtml(string myUrl, out string cookies) {
            HttpHelper http = new HttpHelper();
            HttpItem item = new HttpItem() {
                URL = myUrl,//URL     必需项    
                Method = "get",//URL     可选项 默认为Get   
                Expect100Continue = false,
                Allowautoredirect = false,
                KeepAlive = true,
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.102 Safari/537.36",
                ContentType = "application/x-www-form-urlencoded",//返回类型    可选项有默认值   
                Timeout = HTML_TIMEOUT,
                //Cookie = cookieString,
            };
            item.Header.Add("AcceptEncoding", "gzip,deflate");
            HttpResult result = http.GetHtml(item);
            string html = result.Html;
            NetworkSpeed.increment(Encoding.Default.GetBytes(result.Html).Length);
            cookies = (result.Cookie == null) ? "" : result.Cookie.Split(';')[0];
            return html;
        }
        public static string getHtml(string myUrl) {
            HttpHelper http = new HttpHelper();
            HttpItem item = new HttpItem() {
                URL = myUrl,//URL     必需项    
                Method = "get",//URL     可选项 默认为Get   
                Expect100Continue = false,
                Allowautoredirect = false,
                KeepAlive = true,
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.102 Safari/537.36",
                ContentType = "application/x-www-form-urlencoded",//返回类型    可选项有默认值   
                Timeout = HTML_TIMEOUT,
                //Cookie = cookieString,
            };
            item.Header.Add("AcceptEncoding", "gzip,deflate");
            HttpResult result = http.GetHtml(item);
            NetworkSpeed.increment(Encoding.Default.GetBytes(result.Html).Length);
            string html = result.Html;
            return html;
        }
        /// <summary>
        /// 保存web图片到本地
        /// </summary>
        /// <param name="imgUrl">web图片路径</param>
        /// <param name="path">保存路径</param>
        /// <param name="fileName">保存文件名</param>
        /// <returns></returns>
        public static async Task<string> SaveImageFromWeb(string imgUrl, string path, string fileName, httpParameter MyHttpParameter) {
            return await Task.Run(() => {
                var watch = new Stopwatch();
                watch.Start();
                if (path.Equals("")) throw new Exception("未指定保存文件的路径");
                string imgName = imgUrl.ToString().Substring(imgUrl.ToString().LastIndexOf("/") + 1);
                //string defaultType = ".jpg";
                //string[] imgTypes = new string[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
                string imgType = imgUrl.ToString().Substring(imgUrl.ToString().LastIndexOf("."));
                /*foreach (string it in imgTypes)
                {
                    if (imgType.ToLower().Equals(it)) break;
                    if (it.Equals(".bmp")) imgType = defaultType;
                }*/
                try {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(imgUrl);
                    request.UserAgent = "Mozilla / 5.0(Windows NT 10.0; Win64; x64; rv: 55.0) Gecko / 20100101 Firefox / 55.0";
                    request.ContentType = "text/html";
                    request.Timeout = IMG_TIMEOUT;
                    request.ContinueTimeout = IMG_TIMEOUT;
                    request.ReadWriteTimeout = IMG_TIMEOUT;
                    request.ServicePoint.Expect100Continue = false;//加快载入速度
                    request.ServicePoint.UseNagleAlgorithm = false;//禁止Nagle算法加快载入速度
                    request.ServicePoint.ConnectionLimit = int.MaxValue;//定义最大连接数
                    request.Headers.Add("Cookie", MyHttpParameter.cookie);
                    //request.Host = MyHttpParameter.host;
                    request.Referer = MyHttpParameter.referer;
                    //request.CookieContainer = CookiesContainer;//附加Cookie容器
                    using (var response = (HttpWebResponse)request.GetResponse()) {
                        /* request.CookieContainer = new CookieContainer();
                         foreach (Cookie cookie in response.Cookies)
                         {
                             Console.WriteLine(cookie.ToString());
                             CookiesContainer.Add(cookie);//将Cookie加入容器，保存登录状态
                         }*/
                        Stream stream = null;
                        // 如果页面压缩，则解压数据流
                        if (response.ContentEncoding == "gzip") {
                            Stream responseStream = response.GetResponseStream();
                            if (responseStream != null) {
                                stream = new GZipStream(responseStream, CompressionMode.Decompress);
                            }
                        } else {
                            stream = response.GetResponseStream();
                        }
                        using (stream) {
                            watch.Stop();
                            var milliseconds = watch.ElapsedMilliseconds;//获取请求执行时间
                            if (response.ContentType.ToLower().StartsWith("image/")) {
                                byte[] arrayByte = new byte[1024];
                                int imgLong = (int)response.ContentLength;
                                int l = 0;
                                if (fileName == "") fileName = imgName.Substring(0, imgName.ToString().LastIndexOf("."));
                                using (var fso = new FileStream(path + fileName + imgType, FileMode.Create)) {
                                    while (l < imgLong) {
                                        int i = stream.Read(arrayByte, 0, 1024);
                                        fso.Write(arrayByte, 0, i);
                                        l += i;
                                    }
                                }
                                NetworkSpeed.increment(imgLong);
                                return "Name:" + fileName + imgType + " Time:" + milliseconds + "ms";
                            } else {
                                throw new Exception("not image");
                            }
                        }
                    }
                } catch (Exception ex) {
                    return "Error:" + ex.Message;
                }
            });
        }
        /// <summary>
        /// 用正则匹配数组
        /// </summary>
        /// <param name="text">要匹配的字符串</param>
        /// <param name="pattern">正则表达式</param>
        /// <returns></returns>
        public static string[] regArr(string text, string pattern) {
            Regex regImg = new Regex(pattern, RegexOptions.IgnoreCase);
            MatchCollection matches = regImg.Matches(text);
            int i = 0;
            string[] matchList = new string[matches.Count];
            // 取得匹配项列表             
            foreach (Match match in matches)
                matchList[i++] = match.Groups["matchStr"].Value;
            return matchList;
        }
        public static string getHtmlTitle(string sHtmlText) {
            Regex reg = new Regex(@"<title>([^<]*)</title>");
            Match match = reg.Match(sHtmlText);
            return match.Groups[1].Value;
        }
        private static Image byteArrayToImage(byte[] Bytes) {
            MemoryStream ms = new MemoryStream(Bytes);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
        public static async Task<string> getImg(string imgUrl, string path, string fileName, httpParameter MyHttpParameter) {
            return await Task.Run(() => {
                try {
                    var watch = new Stopwatch();
                    watch.Start();
                    if (path.Equals("")) throw new Exception("未指定保存文件的路径");
                    string imgName = imgUrl.ToString().Substring(imgUrl.ToString().LastIndexOf("/") + 1);
                    string imgType = "";
                    HttpHelper http = new HttpHelper();
                    HttpItem item = new HttpItem() {
                        URL = imgUrl,//URL     必需项    
                        Method = "get",//URL     可选项 默认为Get    
                        Timeout = IMG_TIMEOUT,//连接超时时间     可选项默认为100000 
                        ReadWriteTimeout = IMG_TIMEOUT,
                        UserAgent = "Mozilla / 5.0(Windows NT 10.0; Win64; x64; rv: 55.0) Gecko / 20100101 Firefox / 55.0",//用户的浏览器类型，版本，操作系统     可选项有默认值   
                        ContentType = "text/html",//返回类型    可选项有默认值    
                        Cookie = MyHttpParameter.cookie,
                        Host = MyHttpParameter.host,
                        Referer = MyHttpParameter.referer,
                        ResultType = ResultType.Byte
                    };
                    HttpResult result = http.GetHtml(item);
                    watch.Stop();
                    if (result.ResultByte == null) { throw new Exception(result.Html); }
                    if(result.ResultByte.Length < MIN_IMG_SIZE) { throw new Exception("Invalid image size"); }
                    NetworkSpeed.increment(result.ResultByte.Length);
                    NetworkSpeed.addTotalSize(result.ResultByte.Length);
                    var milliseconds = watch.ElapsedMilliseconds;//获取请求执行时间
                    if (fileName == "") fileName = imgName.Substring(0, imgName.ToString().LastIndexOf("."));
                    //if (File.Exists(path + fileName + imgType)) File.Delete(path + fileName + imgType);
                    using (Image resultImg = byteArrayToImage(result.ResultByte)) {
                        if (resultImg.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg)) {
                            imgType = ".jpg";
                        } else if (resultImg.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp)) {
                            imgType = ".bmp";
                        } else if (resultImg.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif)) {
                            imgType = ".gif";
                        } else if (resultImg.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Tiff)) {
                            imgType = ".tif";
                        } else if (resultImg.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Png)) {
                            imgType = ".png";
                        } else if (resultImg.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Wmf)) {
                            imgType = ".wmf";
                        } else {
                            throw new Exception("Invalid image type");
                        }
                        string imgFullPath = path + fileName + imgType;
                        switch (imgType) {
                            case ".jpg":
                                resultImg.Save(imgFullPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                                break;
                            case ".bmp":
                                resultImg.Save(imgFullPath, System.Drawing.Imaging.ImageFormat.Bmp);
                                break;
                            case ".gif":
                                resultImg.Save(imgFullPath, System.Drawing.Imaging.ImageFormat.Gif);
                                break;
                            case ".tif":
                                resultImg.Save(imgFullPath, System.Drawing.Imaging.ImageFormat.Tiff);
                                break;
                            case ".png":
                                resultImg.Save(imgFullPath, System.Drawing.Imaging.ImageFormat.Png);
                                break;
                            case ".wmf":
                                resultImg.Save(imgFullPath, System.Drawing.Imaging.ImageFormat.Wmf);
                                break;
                        }
                    }
                    return "Name:" + fileName + imgType + " Time:" + milliseconds + "ms";
                } catch (Exception ex) {
                    return "Error:" + ex.Message;
                }
            });
        }
    }
}
