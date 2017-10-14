using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDownloader
{

    interface IWebSiteInfo
    {
        string urlConvert(string domain, string imgType, long pageIndex);
        string[] imgReg(string html);
        string[] pageReg(string html);
    }

    public class MaomiAV : IWebSiteInfo
    {
        public string urlConvert(string domain, string imgType, long pageIndex) {
            return domain + "htm/" + imgType + "/" + pageIndex + ".htm";
        }
        public string[] imgReg(string html) {
            return MyHttp.regArr(
                html,
                @"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<matchStr>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>"
            );
        }
        public string[] pageReg(string html) {
            return MyHttp.regArr(
                html,
                @"<li><a\b[^<>]*?\bhref[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<matchStr>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*target=""_blank"""
            );
        }
    }
    public class _2017MN : IWebSiteInfo
    {
        public string urlConvert(string domain, string imgType, long pageIndex) {
            return domain + "tupianqu/" + imgType + "/index" + (pageIndex == 1 ? "" : "_" + pageIndex) + ".html";
        }
        public string[] imgReg(string html) {
            return MyHttp.regArr(
                html,
                @"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<matchStr>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>"
            );
        }
        public string[] pageReg(string html) {
            return MyHttp.regArr(
                html,
                @"<li><a\b[^<>]*?\bhref[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<matchStr>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*target=""_blank""><span>"
            );
        }
    }
    public class SeGeGe : IWebSiteInfo
    {
        public string urlConvert(string domain, string imgType, long pageIndex) {
            return domain + "pic/" + imgType + "/" + (pageIndex == 1 ? "" : "p_" + pageIndex + ".html");
        }
        public string[] imgReg(string html) {
            return MyHttp.regArr(
                html,
                @"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<matchStr>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>"
            );
        }
        public string[] pageReg(string html) {
            return MyHttp.regArr(
                html,
                @"<li><a\b[^<>]*?\bhref[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<matchStr>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*target=""_blank"""
            );
        }
    }
    public class QianBaiLu : IWebSiteInfo
    {
        public string urlConvert(string domain, string imgType, long pageIndex) {
            return domain + "html/tupian/" + imgType + "/index" + (pageIndex == 1 ? "" : "_" + pageIndex) + ".html";
        }
        public string[] imgReg(string html) {
            return MyHttp.regArr(
                html,
                @"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<matchStr>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>"
            );
        }
        public string[] pageReg(string html) {
            return MyHttp.regArr(
                html,
                @"<li><a\b[^<>]*?\bhref[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<matchStr>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*target=""_blank"""
            );
        }
    }
    public class WuYueXiang : IWebSiteInfo
    {
        public string urlConvert(string domain, string imgType, long pageIndex) {
            return domain + "lm/1-1-3-" + imgType + "-" + pageIndex + ".html?tu=2";
        }
        public string[] imgReg(string html) {
            return MyHttp.regArr(
                html,
                @"<img\b[^<>]*?\b(src|file)[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<matchStr>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>"
            );
        }
        public string[] pageReg(string html) {
            return MyHttp.regArr(
                html,
                @"<a\b[^<>]*?\bhref[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<matchStr>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*target=""_blank"" alt="""
            );
        }
    }
    public class TaoHuaZu : IWebSiteInfo
    {
        public string urlConvert(string domain, string imgType, long pageIndex) {
            return domain + "forum-" + imgType + "-" + pageIndex + ".html";
        }
        public string[] imgReg(string html) {
            return MyHttp.regArr(
                html,
                @"<img id\b[^<>]*?\b(src|file)[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<matchStr>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>"
            );
        }
        public string[] pageReg(string html) {
            return MyHttp.regArr(
                html,
                @"<a\b[^<>]*?\bhref[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<matchStr>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*onclick=""atarget"
            );
        }
    }
}
