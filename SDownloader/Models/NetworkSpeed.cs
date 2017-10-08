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

        private static double totalSize;

        static List<Speed> speedList = new List<Speed>();

        public static void init() {
            totalSize = 0;
        }

        public static void increment(long value) {
            lock (thislock) {
                speedList.Add(new Speed() { Size = value, Time = DateTime.Now });
                if (speedList.Count > 3000) {
                    if (speedList.Count > 0) {
                        speedList.RemoveAt(0);
                    }
                }
            }
        }

        public static void addTotalSize(long size) {
            lock (thislock) {
                totalSize += size;
            }
        }

        static DateTime lastGetSpeedTime = DateTime.Now;

        public static string getSpeed() {
            lock (thislock) {
                if (speedList.Count > 0 && DateTime.Now.Subtract(speedList[speedList.Count - 1].Time).TotalSeconds > 1) {
                    speedList.Clear();
                    lastGetSpeedTime = DateTime.Now;
                }
                lastGetSpeedTime = DateTime.Now;
                if (speedList.Count == 0) return "0 B/s";
                long total = 0;
                for (int i = 0; i < speedList.Count; i++) {
                    total += speedList[i].Size;
                }
                double speed = total / (DateTime.Now - speedList[0].Time).TotalSeconds;
                return string.Format("{0}/s", getText(speed));
            }
        }

        private static string getText(double size) {
            if (size < 1024)
                return string.Format("{0} B", size.ToString("0.0"));
            else if (size < 1024 * 1024)
                return string.Format("{0} KB", (size / 1024.0f).ToString("0.0"));
            else if (size < 1024 * 1024 * 1024)
                return string.Format("{0} MB", (size / (1024.0f * 1024.0f)).ToString("0.0"));
            else
                return string.Format("{0} GB", (size / (1024.0f * 1024.0f * 1024.0f)).ToString("0.0"));
        }

        public static string getTotalSizeText() {
            return getText(totalSize);
        }
    }
}
