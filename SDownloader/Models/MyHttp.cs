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
using System.Security.Cryptography;

namespace SDownloader
{
    public class MyHttp
    {
        const int HTML_TIMEOUT = 10000;
        const int IMG_TIMEOUT = 20000;
        const int MIN_IMG_SIZE = 5000;
        static CookieContainer CookiesContainer { get; set; }//定义Cookie容器
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
                Accept = "application / json, text / javascript, */*; q=0.01",
                //Cookie = cookieString,
            };
            item.Header.Add("AcceptEncoding", "gzip,deflate");
            HttpResult result = http.GetHtml(item);
            string html = result.Html;
            NetworkSpeed.increment(Encoding.Default.GetBytes(result.Html).Length);
            html = html.Replace(@"\s", string.Empty).Replace(@"\r", string.Empty).Replace(@"\n", string.Empty).Replace(@"\f", string.Empty);
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
            html = html.Replace(@"\s", string.Empty).Replace(@"\r", string.Empty).Replace(@"\n", string.Empty).Replace(@"\f", string.Empty);
            return html;
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
                    HttpHelper http = new HttpHelper();
                    HttpItem item = new HttpItem() {
                        URL = imgUrl,//URL     必需项    
                        Method = "get",//URL     可选项 默认为Get    
                        Timeout = IMG_TIMEOUT,//连接超时时间     可选项默认为100000 
                        ReadWriteTimeout = IMG_TIMEOUT,
                        UserAgent = "Mozilla / 5.0(Windows NT 10.0; Win64; x64; rv: 55.0) Gecko / 20100101 Firefox / 55.0",//用户的浏览器类型，版本，操作系统     可选项有默认值   
                        ContentType = "text/html",//返回类型    可选项有默认值    
                        //Cookie = MyHttpParameter.cookie,
                        //Host = MyHttpParameter.host,
                        //Referer = MyHttpParameter.referer,
                        ResultType = ResultType.Byte
                    };
                    HttpResult result = http.GetHtml(item);
                    watch.Stop();
                    if (result.ResultByte == null) { throw new Exception(result.Html); }
                    if (result.ResultByte.Length < MIN_IMG_SIZE) { throw new Exception("Invalid image size:" + result.ResultByte.Length); }
                    NetworkSpeed.increment(result.ResultByte.Length);
                    NetworkSpeed.addTotalSize(result.ResultByte.Length);
                    var milliseconds = watch.ElapsedMilliseconds;//获取请求执行时间
                    string saveResult = saveImg(result.ResultByte, path, fileName, imgName);
                    if (!saveResult.StartsWith("Error:")) {
                        return "Name:" + saveResult + " Time:" + milliseconds + "ms";
                    } else {
                        throw new Exception(saveResult);
                    }
                } catch (Exception ex) {
                    return (ex.Message.StartsWith("Error:") ? "" : "Error:") + ex.Message + " ImgUrl:" + imgUrl;
                }
            });
        }
        private static string saveImg(byte[] imgByte, string path, string fileName, string imgName) {
            try {
                if (fileName == "") fileName = imgName.Substring(0, imgName.ToString().LastIndexOf("."));
                string imgType = "";
                //if (File.Exists(path + fileName + imgType)) File.Delete(path + fileName + imgType);
                using (Image resultImg = byteArrayToImage(imgByte)) {
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
                return fileName + imgType;
            } catch (Exception ex) {
                return "Error:" + ex.Message;
            }
        }
        public static string get16bitMd5Str(string ConvertString) {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string re = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(ConvertString)), 4, 8);
            re = re.Replace("-", "");
            return re;
        }
        public static string getValidFileName(string fileName) {
            StringBuilder rBuilder = new StringBuilder(fileName);
            foreach (char rInvalidChar in Path.GetInvalidFileNameChars())
                rBuilder.Replace(rInvalidChar.ToString(), string.Empty);
            return rBuilder.ToString();
        }
        public static string[] getHtmlImg(string sHtmlText) {
            string reStr = string.Empty;
            MatchCollection mc = Regex.Matches(sHtmlText, @"<img\b[^<>]*?\b(src|file)[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*[^\s\t\r\n""'<>]*[^<>]*?[\s\t\r\n]*>");
            foreach (Match m in mc) {
                reStr += m + "\t";
            }
            if (reStr.Length > 1) {
                reStr = reStr.Substring(0, reStr.Length - 1);
            }
            return Regex.Split(reStr,"\t");
        }
        public static string[] getHtmlImgWithKey(string[] imgs, string[] keys) {
            string reStr = string.Empty;
            foreach (string imgStr in imgs) {
                if (imgStr.Length > 0) {
                    if (string.Join(string.Empty, keys).Length == 0) {
                        reStr += regArr(imgStr, @"<img\b[^<>]*?\b(src|file)[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<matchStr>[^\s\t\r\n""'<>]*)[^<>]*?[\s\t\r\n]*>")[0] + "\t";
                    } else {
                        bool isAdd = true;
                        foreach (string keyStr in keys) {
                            if (keyStr != string.Empty & imgStr.IndexOf(keyStr) == -1) {
                                isAdd = false;
                            }
                        }
                        if(isAdd) reStr += regArr(imgStr, @"<img\b[^<>]*?\b(src|file)[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<matchStr>[^\s\t\r\n""'<>]*)[^<>]*?[\s\t\r\n]*>")[0] + "\t";
                    }
                }
            }
            if (reStr.Length > 1) {
                reStr = reStr.Substring(0, reStr.Length - 1);
            }
            return Regex.Split(reStr, "\t"); ;
        }
    }
}
