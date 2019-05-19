using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace NorskTipping
{
    public class ToJsonBase
    {
        public GameInit Init;
        public int CurrentRound => (int)Math.Floor((DateTime.Today.Subtract(Init.InitialDate).TotalDays - 1) / 7) + 1;
        public List<GameResultModel> Model = new List<GameResultModel>();
        public static string[] GetNumbers(string path, int round)
        {            
            return GetNumbers(File.ReadAllText($@"{path}\{round}.txt"));
        }

        public (string[] Numbers, string DrawDate) GetNumbersWithDate(string path, int round)
        {
            var text = File.ReadAllText($@"{path}\{round}.txt");
            return (GetNumbers(text), GetDrawDate(text));
        }

        private static string[] GetNumbers(string lottoRaw)
        {
            return Regex.Matches(lottoRaw, @"\[.*?\]").Cast<Match>().Select(m => m.Value.Replace("[", "").Replace("]", "")).ToArray();
        }

        private string GetDrawDate(string lottoRaw)
        {
            const string drawString = "drawDate";
            const string unsortedMainTable = "unsortedMainTable";
            var startPos = lottoRaw.IndexOf(drawString) + drawString.Length + 3;
            var length = lottoRaw.IndexOf(unsortedMainTable) - startPos - 3;
            return lottoRaw.Substring(startPos, length);
        }
    }
}
