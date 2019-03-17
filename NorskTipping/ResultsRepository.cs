using System;
using System.IO;
using System.Linq;
using System.Net;

namespace NorskTipping
{
    public class ResultsRepository
    {       
        public static int Current = 1193;

        public static FileInfo[] ReadAllFiles(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new Exception("The path to XML files cannot be empty.");
            var di = new DirectoryInfo(path);
            return di.GetFiles("*.txt");
        }

        public static void FetchResultsToDisk(string path)
        {
            var di = ReadAllFiles(path);
            using (var webClient = new WebClient())
            {
                for (var i = Current; i > 0; i--)
                {
                    var file = di.FirstOrDefault(a => a.Name == i + ".txt");
                    if (file != null)
                        break;
                    var json = webClient.DownloadString("https://www.norsk-tipping.no/api-lotto/getResultInfo.json?drawID=" + i);
                    File.WriteAllText(path + i + ".txt", json);
                }
            }
        }
    }
}
