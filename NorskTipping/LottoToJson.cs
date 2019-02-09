using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace NorskTipping
{
    public class LottoToJson
    {
        private string _start = "mainTable";
        private string _end = "addTable";

        public LottoToJson(bool sorted)
        {
            if (!sorted)
            {
                _start = "unsortedMainTable";
                _end = "unsortedAddTable";
            }
        }

        public string Do(int rounds)
        {
            var start = PreviousResults.Current - rounds;
            var labels = Enumerable.Range(start, PreviousResults.Current - start + 1).Reverse().ToList();
            var model = GetResultsModel(labels);
            return $"lottoData = JSON.parse('{JsonConvert.SerializeObject(model)}'); labels = JSON.parse('{JsonConvert.SerializeObject(labels.Select(x => x.ToString()))}');";
        }

        public string GetNumbers(int round)
        {            
            return GetNumbers(File.ReadAllText($@"c:\NorskTipping\{round}.txt"));
        }

        public List<LottoResults> GetResultsModel(List<int> labels)
        {
            var lot = new List<LottoResults>();
            for (var i = 1; i < 8; i++)
            {
                lot.Add(new LottoResults{Label = "Ball nr. "  + i, Data = new ArrayList()});
            }
            
            lot[0].BorderColor = "red";
            lot[1].BorderColor = "blue";
            lot[2].BorderColor = "brown";
            lot[3].BorderColor = "green";
            lot[4].BorderColor = "black";
            lot[5].BorderColor = "violet";
            lot[6].BorderColor = "pink";

            foreach (var i in labels)
            {
                var res = GetNumbers(i);
                var numbers = res.Split(',').Select(int.Parse).ToList();
                for (var j = 0; j < numbers.Count; j++)
                    lot[j].Data.Add(numbers[j]);                          
            }
            return lot;
        }
        
        public string GetNumbers(string lotto)
        {
            var startPos = lotto.LastIndexOf(_start) + _start.Length + 3;
            var length = lotto.IndexOf(_end) - startPos - 3;
            var res = lotto.Substring(startPos, length);
            return res;
        }
    }
}
