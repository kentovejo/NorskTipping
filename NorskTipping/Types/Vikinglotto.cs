using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace NorskTipping
{
    public class Vikinglotto: ToJsonBase, IGame
    {
        public Vikinglotto()
        {
            Init = new GameInit{ Name = "VikingLotto", InitialDate = new DateTime(1996,5,9), EndPoint = Endpoints.VikingLotto};
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

            foreach (var i in labels)
            {
                var res = GetNumbers(path + Init.Name, i);
                var numbers = res[0].Split(',').Select(int.Parse);
                if (sorted)
                    numbers = numbers.OrderBy(q => q);
                var numberList = numbers.ToList();
                numberList.AddRange(res[1].Split(',').Select(int.Parse));
                for (var j = 0; j < numberList.Count; j++)
                    Model[j].Data.Add(numberList[j]);                          
            }
        }
    }
}
