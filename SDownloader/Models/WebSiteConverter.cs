using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDownloader
{
    public static class WebSiteConverter
    {
        public static string getUrl(string urlPattern, string firstPageUrlPattern, string domain, string imgType, long pageIndex) {
            if (pageIndex == 1 && firstPageUrlPattern != "") {
                return firstPageUrlPattern.Replace("<domain>", domain).Replace("<imgType>", imgType).Replace("<pageIndex>", pageIndex.ToString());
            } else {
                return urlPattern.Replace("<domain>", domain).Replace("<imgType>", imgType).Replace("<pageIndex>", pageIndex.ToString());
            }
        }
        public static string[] pageReg(string html, string pagePattern) {
            return MyHttp.regArr(html, pagePattern);
        }
        public static string[] imgReg(string html, string[] imgKeys) {
            return MyHttp.getHtmlImgWithKey(MyHttp.getHtmlImg(html), imgKeys);
        }
    }

    public interface IWebsiteConverter
    {
        string urlConvert(string domain, string imgType, long pageIndex);
        string[] imgReg(string html);
        string[] pageReg(string html);
    }

    public class MaomiAV : IWebsiteConverter
    {
        private string[] imgKeys = new string[] { string.Empty };
        private string pagePattern = @"<li><a\b[^<>]*?\bhref[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<matchStr>[^\s\t\r\n""'<>]*)[^<>]*?[\s\t\r\n]*target=""_blank""";
        public string urlConvert(string domain, string imgType, long pageIndex) {
            return domain + "htm/" + imgType + "/" + pageIndex + ".htm";
        }
        public string[] imgReg(string html) {
            return MyHttp.getHtmlImgWithKey(MyHttp.getHtmlImg(html), imgKeys);
        }
        public string[] pageReg(string html) {
            return MyHttp.regArr(html, pagePattern);
        }
    }
    public class _2017MN : IWebsiteConverter
    {
        private string[] imgKeys = new string[] { string.Empty };
        private string pagePattern = @"<li><a\b[^<>]*?\bhref[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<matchStr>[^\s\t\r\n""'<>]*)[^<>]*?[\s\t\r\n]*target=""_blank""><span>";
        public string urlConvert(string domain, string imgType, long pageIndex) {
            return domain + "tupianqu/" + imgType + "/index" + (pageIndex == 1 ? "" : "_" + pageIndex) + ".html";
        }
        public string[] imgReg(string html) {
            return MyHttp.getHtmlImgWithKey(MyHttp.getHtmlImg(html), imgKeys);
        }
        public string[] pageReg(string html) {
            return MyHttp.regArr(html, pagePattern);
        }
    }
    public class SeGeGe : IWebsiteConverter
    {
        private string[] imgKeys = new string[] { string.Empty };
        private string pagePattern = @"<li><a\b[^<>]*?\bhref[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<matchStr>[^\s\t\r\n""'<>]*)[^<>]*?[\s\t\r\n]*target=""_blank""";
        public string urlConvert(string domain, string imgType, long pageIndex) {
            return domain + "pic/" + imgType + "/" + (pageIndex == 1 ? "" : "p_" + pageIndex + ".html");
        }
        public string[] imgReg(string html) {
            return MyHttp.getHtmlImgWithKey(MyHttp.getHtmlImg(html), imgKeys);
        }
        public string[] pageReg(string html) {
            return MyHttp.regArr(html, pagePattern);
        }
    }
    public class QianBaiLu : IWebsiteConverter
    {
        private string[] imgKeys = new string[] { string.Empty };
        private string pagePattern = @"<li><a\b[^<>]*?\bhref[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<matchStr>[^\s\t\r\n""'<>]*)[^<>]*?[\s\t\r\n]*target=""_blank""";
        public string urlConvert(string domain, string imgType, long pageIndex) {
            return domain + "html/tupian/" + imgType + "/index" + (pageIndex == 1 ? "" : "_" + pageIndex) + ".html";
        }
        public string[] imgReg(string html) {
            return MyHttp.getHtmlImgWithKey(MyHttp.getHtmlImg(html), imgKeys);
        }
        public string[] pageReg(string html) {
            return MyHttp.regArr(html, pagePattern);
        }
    }
    public class WuYueXiang : IWebsiteConverter
    {
        private string[] imgKeys = new string[] { string.Empty };
        private string pagePattern = @"<a\b[^<>]*?\bhref[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<matchStr>[^\s\t\r\n""'<>]*)[^<>]*?[\s\t\r\n]*target=""_blank"" alt=""";
        public string urlConvert(string domain, string imgType, long pageIndex) {
            return domain + "lm/" + imgType + "-" + pageIndex + ".html?tu=2";
        }
        public string[] imgReg(string html) {
            return MyHttp.getHtmlImgWithKey(MyHttp.getHtmlImg(html), imgKeys);
        }
        public string[] pageReg(string html) {
            return MyHttp.regArr(html, pagePattern);
        }
    }
    public class TaoHuaZu : IWebsiteConverter
    {
        private string[] imgKeys = new string[] { "id" };
        private string pagePattern = @"<a\b[^<>]*?\bhref[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<matchStr>[^\s\t\r\n""'<>]*)[^<>]*?[\s\t\r\n]*onclick=""atarget";
        public string urlConvert(string domain, string imgType, long pageIndex) {
            return domain + "forum-" + imgType + "-" + pageIndex + ".html";
        }
        public string[] imgReg(string html) {
            return MyHttp.getHtmlImgWithKey(MyHttp.getHtmlImg(html), imgKeys);
        }
        public string[] pageReg(string html) {
            return MyHttp.regArr(html, pagePattern);
        }
    }
    public class CaoPorn : IWebsiteConverter
    {
        private string[] imgKeys = new string[] { @"id=""album_photo" };
        private string pagePattern = @"<div class=""album_box_new"">[\s\t\r\n]*<a\b[^<>]*?\bhref[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<matchStr>[^\s\t\r\n""'<>]*)[^<>]*?[\s\t\r\n]*>";
        public string urlConvert(string domain, string imgType, long pageIndex) {
            return domain + "albums?c=" + imgType + "&page=" + pageIndex;
        }
        public string[] imgReg(string html) {
            return MyHttp.getHtmlImgWithKey(MyHttp.getHtmlImg(html), imgKeys);
        }
        public string[] pageReg(string html) {
            return MyHttp.regArr(html, pagePattern);
        }
    }
    public class Sex8 : IWebsiteConverter
    {
        private string[] imgKeys = new string[] { @"id=""aimg_" };
        private string pagePattern = @"<a\b[^<>]*?\bhref[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<matchStr>[^\s\t\r\n""'<>]*)[^<>]*?[\s\t\r\n]* class=""s>";
        public string urlConvert(string domain, string imgType, long pageIndex) {
            return domain + "forum-" + imgType + "-" + pageIndex + ".html";
        }
        public string[] imgReg(string html) {
            return MyHttp.getHtmlImgWithKey(MyHttp.getHtmlImg(html), imgKeys);
        }
        public string[] pageReg(string html) {
            return MyHttp.regArr(html, pagePattern);
        }
    }
}
