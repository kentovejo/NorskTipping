using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace NorskTipping
{
    public class LottoToJson : ToJsonBase, IGame
    {
        public LottoToJson()
        {
            Init = new GameInit {Name = "Lotto", InitialDate = new DateTime(1996, 5, 11), EndPoint = Endpoints.Lotto};
        }
        public string Do(string path, int rounds, bool sorted, string filter)
        {
            var start = Init.CurrentRound - rounds;
            var labels = Enumerable.Range(start, Init.CurrentRound - start + 1).Reverse().ToList();
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

            foreach (var i in labels)
            {
                var res = GetNumbers(path + Init.Name, i);
                var numbers = res[0].Split(',').Concat(res[1].Split(',')).Select(int.Parse);
                if (sorted)
                    numbers = numbers.OrderBy(q => q);
                var numberList = numbers.ToList();
                for (var j = 0; j < numberList.Count; j++)
                    Model[j].Data.Add(numberList[j]);                          
            }
        }
    }
}
