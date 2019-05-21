using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace NorskTipping
{
    public class EuroJackpot: ToJsonBase, IGame
    {
        public EuroJackpot()
        {
            Init = new GameModel{ Name = "EuroJackpot", InitialDate = new DateTime(2013,2,6), EndPoint = Endpoints.EuroJackpot};
        }
        public string Do(string path, int rounds, bool sorted, string filter)
        {
            var start = CurrentRound - rounds;
            var labels = Enumerable.Range(start, CurrentRound - start + 1).Reverse().ToList();
            switch (filter)
            {
                case "EVEN":
                    labels = labels.Where(a => a % 2 == 0).ToList();
                    break;
                case "ODD":
                    labels = labels.Where(a => a % 2 == 1).ToList();
                    break;
            }
            GetResultsModel(path, labels, sorted);
            return $"lottoData = JSON.parse('{JsonConvert.SerializeObject(Model)}'); labels = JSON.parse('{JsonConvert.SerializeObject(labels.Select(x => x.ToString()))}');";
        }

        private void GetResultsModel(string path, IEnumerable<int> labels, bool sorted)
        {
            for (var i = 1; i < 8; i++)
            {
                Model.Add(new GameResultModel{Label = "Ball nr. "  + i, Data = new ArrayList()});
            }
            
            Model[0].BorderColor = "red";
            Model[1].BorderColor = "blue";
            Model[2].BorderColor = "brown";
            Model[3].BorderColor = "green";
            Model[4].BorderColor = "black";
            Model[5].BorderColor = "violet";
            Model[6].BorderColor = "pink";
            ConvertToModel(path, labels, sorted);
        }
    }
}
