using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace SDownloader {
    public static class DelSpider {
        enum getMode {
            length,
            count,
        }
        static string myPath = "";
        //static string allRe;
        public static void work() {
            do {
                Console.Write("Input your path:");
                myPath = Console.ReadLine();
                getFolders(myPath);
                Console.Write("Continue:");
            } while (Console.ReadLine().ToLower() == "y");
            Console.WriteLine("===============================================");
            Console.WriteLine("DelSpider Finished");
        }
        public static string Read(string path) {
            string myRe = "";
            StreamReader sr = new StreamReader(path, Encoding.Default);
            String line;
            while ((line = sr.ReadLine()) != null) {
                //Console.WriteLine(line.ToString());
                myRe += line.ToString() + "|";
            }
            return myRe;
        }
        public static void getFolders(string mySourcePath) {
            String[] files = Directory.GetDirectories(mySourcePath, "*");
            //Console.WriteLine(allRe);
            //Directory.GetFiles(path, "*", SearchOption.TopDirectoryOnly);
            foreach (string myPath in files) {
                /*string myNum = myPath.ToString().Substring(myPath.ToString().LastIndexOf(@"\") + 1);
                myNum = myNum.ToString().Substring(0,myNum.ToString().IndexOf("-"));
                //Console.WriteLine(myNum);
                if (allRe.IndexOf(myNum + "|") != -1)
                {
                    DirectoryInfo di = new DirectoryInfo(myPath);
                    di.Delete(true);
                    Console.WriteLine(myPath);
                }*/
                long re = GetDirectory(myPath, getMode.count);
                if (re < 6) {
                    Console.WriteLine(myPath + " " + re);
                    DirectoryInfo di = new DirectoryInfo(myPath);
                    di.Delete(true);
                }
                /*Console.WriteLine(myPath + " " + (myPath.IndexOf(@"【图吧水印】")) + " "  + Directory.Exists(myPath).ToString());
                if (myPath.IndexOf(@"【图吧水印】") != -1 && Directory.Exists(myPath))
                {
                    Console.WriteLine(myPath.Replace("【图吧水印】", ""));
                    System.IO.Directory.Move(myPath,myPath.Replace("【图吧水印】", ""));
                }*/
            }
        }
        /// <summary>
        /// 获取目录信息
        /// </summary>
        /// <param name="dirPath">目录路径</param>
        /// <param name="mode">获取模式</param>
        /// <returns></returns>
        static long GetDirectory(string dirPath, getMode mode) {
            //判断给定的路径是否存在,如果不存在则退出
            if (!Directory.Exists(dirPath))
                return 0;
            long re = 0;

            //定义一个DirectoryInfo对象
            DirectoryInfo di = new DirectoryInfo(dirPath);

            //通过GetFiles方法,获取di目录中的所有文件的大小
            foreach (FileInfo fi in di.GetFiles()) {
                if (fi.Length <= 30000) {
                    Console.WriteLine(fi.FullName);
                    fi.Delete();
                } else {
                    if (mode == getMode.length) {
                        re += fi.Length;

                    } else if (mode == getMode.count) {
                        re++;
                    }
                }
            }
            //获取di中所有的文件夹,并存到一个新的对象数组中,以进行递归
            DirectoryInfo[] dis = di.GetDirectories();
            if (dis.Length > 0) {
                for (int i = 0; i < dis.Length; i++) {
                    re += GetDirectory(dis[i].FullName, mode);
                }
            }
            return re;
        }
    }
}
