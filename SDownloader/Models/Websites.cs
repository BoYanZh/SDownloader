using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDownloader
{
    public class WebSiteSetting
    {
        public string siteName;
        public string domain;
        public string imgType;
        public IWebsiteConverter websiteConverter;
        public string[] imgKeys;
        public string pageRegex;
        public string urlPattern;
        public string firstPageUrlPattern;
    }
    public static class WebsiteInfo
    {

        public static List<WebSiteSetting> websiteList = new List<WebSiteSetting>();
        static WebsiteInfo() {
            websiteList.Add(new WebSiteSetting() {
                siteName = "猫咪AV",
                domain = "https://www.ttt311.com/",
                imgType = "piclist3",
                websiteConverter = new MaomiAV()
            });
            websiteList.Add(new WebSiteSetting() {
                siteName = "千百撸",
                domain = "https://333av.vip/",
                imgType = "oumei",
                websiteConverter = new QianBaiLu()
            });
            websiteList.Add(new WebSiteSetting() {
                siteName = "色哥哥",
                domain = "http://48td.com/",
                imgType = "13",
                websiteConverter = new SeGeGe()
            });
            websiteList.Add(new WebSiteSetting() {
                siteName = "五月香",
                domain = "http://www.dazhuazhi.com/",
                imgType = "1-1-4-68",
                websiteConverter = new WuYueXiang()
            });
            websiteList.Add(new WebSiteSetting() {
                siteName = "2017MN",
                domain = "http://www.2017mn.com/",
                imgType = "oumei",
                websiteConverter = new _2017MN()
            });
            websiteList.Add(new WebSiteSetting() {
                siteName = "桃花族",
                domain = "http://thibt.com/",
                imgType = "221",
                websiteConverter = new TaoHuaZu()
            });
            websiteList.Add(new WebSiteSetting() {
                siteName = "超碰",
                domain = "https://2020.baofee.com/",
                imgType = "0",
                websiteConverter = new CaoPorn()
            });
            websiteList.Add(new WebSiteSetting() {
                siteName = "性吧",
                domain = "http://sohu58bbs.net/",
                imgType = "150",
                websiteConverter = new Sex8()
            });
        }
    }
}
