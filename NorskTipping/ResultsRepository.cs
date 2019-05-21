using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;

namespace NorskTipping
{
    public class ResultsRepository
    {
        private static FileInfo[] ReadAllFiles(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new Exception("The path to XML files cannot be empty.");
            var di = new DirectoryInfo(path);
            return di.GetFiles("*.txt");
        }

        public static void FetchResultsToDisk(string basePath, ToJsonBase game)
        {
            var di = ReadAllFiles(basePath + game.Init.Name);
            using (var webClient = new WebClient())
            {
                for (var i = game.CurrentRound; i > game.CurrentRound - 30; i--)
                {
                    var file = di.FirstOrDefault(a => a.Name == i + ".txt");
                    if (file != null)
                        break;
                    var json = webClient.DownloadString(game.Init.EndPoint + i);
                    File.WriteAllText(basePath + game.Init.Name + "\\" + i + ".txt", json);
                }
            }
        }

        public static string GetRoundResults(string path, int round)
        {
            return File.ReadLines($@"{path}\{round}.txt").Skip(1).Take(1).First();
        }
    }
}
