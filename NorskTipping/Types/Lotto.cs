using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace NorskTipping
{
    public class Lotto : ToJsonBase, IGame
    {
        public Lotto()
        {
            Init = new GameModel {Name = "Lotto", InitialDate = new DateTime(1996, 5, 11), EndPoint = Endpoints.Lotto};
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

        public void GetResultsModel(string path, IEnumerable<int> labels, bool sorted)
        {
            for (var i = 1; i < 9; i++)
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
            Model[7].BorderColor = "orange";
            ConvertToModel(path, labels, sorted);
        }
    }
}
