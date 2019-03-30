using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace NorskTipping
{
    public class LottoToJson
    {
        private const string Start = "unsortedMainTable";
        private const string End = "unsortedAddTable";

        public string Do(string path, int rounds, bool sorted, string filter)
        {
            var start = ResultsRepository.Current - rounds;
            var labels = Enumerable.Range(start, ResultsRepository.Current - start + 1).Reverse().ToList();
            switch (filter)
            {
                case "EVEN":
                    labels = labels.Where(a => a % 2 == 0).ToList();
                    break;
                case "ODD":
                    labels = labels.Where(a => a % 2 == 1).ToList();
                    break;
            }
            var model = GetResultsModel(path, labels, sorted);
            return $"lottoData = JSON.parse('{JsonConvert.SerializeObject(model)}'); labels = JSON.parse('{JsonConvert.SerializeObject(labels.Select(x => x.ToString()))}');";
        }
        
        public string GetNumbers(string path, int round)
        {            
            return GetNumbers(File.ReadAllText($@"{path}{round}.txt"));
        }
        public (string Numbers, string DrawDate) GetNumbersWithDate(string path, int round)
        {
            var text = File.ReadAllText($@"{path}{round}.txt");
            return (GetNumbers(text), GetDrawDate(text));
        }

        public List<LottoResults> GetResultsModel(string path, IEnumerable<int> labels, bool sorted)
        {
            var lot = new List<LottoResults>();
            for (var i = 1; i < 9; i++)
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
            lot[7].BorderColor = "orange";

            foreach (var i in labels)
            {
                var res = GetNumbers(path, i);
                var numbers = res.Split(',').Select(int.Parse);
                if (sorted)
                    numbers = numbers.OrderBy(q => q);
                var numberList = numbers.ToList();
                for (var j = 0; j < numberList.Count; j++)
                    lot[j].Data.Add(numberList[j]);                          
            }
            return lot;
        }
        
        public string GetNumbers(string lottoRaw)
        {
            var normal = ExtractNumbers(lottoRaw, Start, End);
            var extra = ExtractNumbers(lottoRaw, End, "mainTable");
            return normal+ "," + extra;
        }
        
        private string ExtractNumbers(string lottoRaw, string start, string end)
        {
            var startPos = lottoRaw.LastIndexOf(start) + start.Length + 3;
            var length = lottoRaw.IndexOf(end) - startPos - 3;
            return lottoRaw.Substring(startPos, length);
        }

        public string GetDrawDate(string lottoRaw)
        {
            var drawString = "drawDate";
            var unsortedMainTable = "unsortedMainTable";
            var startPos = lottoRaw.IndexOf(drawString) + drawString.Length + 3;
            var length = lottoRaw.IndexOf(unsortedMainTable) - startPos - 3;
            return lottoRaw.Substring(startPos, length);
        }
    }
}
