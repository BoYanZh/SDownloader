using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDownloader
{
    public static class NetworkSpeed
    {
        private struct Speed
        {
            public DateTime Time;
            public long Size;
        }
        private static object thislock = new Object();

        static List<Speed> SpeedList = new List<Speed>();

        public static void Increment(long value) {
            lock (thislock) {
                SpeedList.Add(new Speed() { Size = value, Time = DateTime.Now });
                if (SpeedList.Count > 3000) {
                    if (SpeedList.Count > 0) {
                        SpeedList.RemoveAt(0);
                    }
                }
            }
        }

        static DateTime LastGetSpeedTime = DateTime.Now;

        public static string GetSpeed() {
            lock (thislock) {
                if (SpeedList.Count > 0 && DateTime.Now.Subtract(SpeedList[SpeedList.Count - 1].Time).TotalSeconds > 1) {
                    SpeedList.Clear();
                    LastGetSpeedTime = DateTime.Now;
                }
                LastGetSpeedTime = DateTime.Now;
                if (SpeedList.Count == 0) return "0 B/s";
                long total = 0;
                for (int i = 0; i < SpeedList.Count; i++) {
                    total += SpeedList[i].Size;
                }
                double speed = total / (DateTime.Now - SpeedList[0].Time).TotalSeconds;
                return string.Format("{0}/s", GetText(speed));
            }
        }

        private static string GetText(double size) {
            if (size < 1024)
                return string.Format("{0} B", size.ToString("0.0"));
            else if (size < 1024 * 1024)
                return string.Format("{0} KB", (size / 1024.0f).ToString("0.0"));
            else
                return string.Format("{0} MB", (size / (1024.0f * 1024.0f)).ToString("0.0"));
        }
    }
}
