using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace NorskTipping
{
    public class Keno: ToJsonBase, IGame
    {
        private new int CurrentRound => (int)Math.Floor((DateTime.Today.Subtract(Init.InitialDate).TotalDays - 1) / 7) + 1;
        public Keno()
        {
            Init = new GameInit{ Name = "Keno", InitialDate = new DateTime(2013,2,6), EndPoint = Endpoints.Keno};
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
            for (var i = 1; i < 21; i++)
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
            Model[8].BorderColor = "purple";
            Model[9].BorderColor = "beige";
            Model[10].BorderColor = "red";
            Model[11].BorderColor = "blue";
            Model[12].BorderColor = "brown";
            Model[13].BorderColor = "green";
            Model[14].BorderColor = "black";
            Model[15].BorderColor = "violet";
            Model[16].BorderColor = "pink";
            Model[17].BorderColor = "orange";
            Model[18].BorderColor = "purple";
            Model[19].BorderColor = "beige";

            foreach (var i in labels)
            {
                var res = GetNumbers(path + Init.Name, i);
                var numbers = res[5].Split(',').Select(int.Parse);
                if (sorted)
                    numbers = numbers.OrderBy(q => q);
                var numberList = numbers.ToList();
                //numberList.AddRange(res[7].Split(',').Select(int.Parse));
                for (var j = 0; j < numberList.Count; j++)
                    Model[j].Data.Add(numberList[j]);                          
            }
        }
    }
}
