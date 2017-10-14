using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDownloader.Event
{
    public class OnFetchedEventArgs
    {
        public GetSpider.ImgInfo imgInfoResult { get; set; }
        public OnFetchedEventArgs(GetSpider.ImgInfo imgInfo) {
            imgInfoResult = imgInfo;
        }
    }
    public class OnFinishedEventArgs
    {
        public GetSpider.ImgInfo imgInfoResult { get; set; }
        public OnFinishedEventArgs(GetSpider.ImgInfo imgInfo) {
            imgInfoResult = imgInfo;
        }
    }
}
