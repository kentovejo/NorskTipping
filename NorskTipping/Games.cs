using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace NorskTipping
{
    public class Games
    {
        public readonly List<IGame> GameTypes = new List<IGame>();

        public Games()
        {
            GameTypes.Add(new Lotto());
            GameTypes.Add(new Vikinglotto());
            GameTypes.Add(new EuroJackpot());
        }

        public string Do(int index, string path, int rounds, bool sorted, string filter)
        {
            return GameTypes[index].Do(path, rounds, sorted, filter);
        }

        public void FetchResultsToDisk(string basePath, int gameIndex)
        {
            var game = (ToJsonBase) GameTypes[gameIndex];
            var di = ResultsRepository.ReadAllFiles(basePath + game.Init.Name);
            using (var webClient = new WebClient())
            {
                for (var i = game.CurrentRound; i > 0; i--)
                {
                    var file = di.FirstOrDefault(a => a.Name == i + ".txt");
                    if (file != null)
                        break;
                    var json = webClient.DownloadString(game.Init.EndPoint + i);
                    File.WriteAllText(basePath + game.Init.Name + "\\" + i + ".txt", json);
                }
            }
        }
    } 
}
