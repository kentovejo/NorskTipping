using System;
using System.Collections.Generic;

namespace NorskTipping
{
    public class BasicGame
    {
        public GameModel Init;
        public int CurrentRound => (int)Math.Floor((DateTime.Today.Subtract(Init.InitialDate).TotalDays - 1) / 7) + 1;
        public List<GameResultModel> Model = new List<GameResultModel>();
        public static GameRoundResultModel GetNumbers(string path, int round)
        {            
            return new GameRoundResult().GetRound(path, round);
        }
        public void ConvertToModel(string path, IEnumerable<int> labels, bool sorted)
        {
            foreach (var i in labels)
            {
                var res = GetNumbers(path + Init.Name, i);
                if (sorted)
                    res.UnsortedMainTable.Sort();
                if(res.AddTable != null)
                    res.UnsortedMainTable.AddRange(res.AddTable);
                for (var j = 0; j < res.UnsortedMainTable.Count; j++)
                    Model[j].Data.Add(res.UnsortedMainTable[j]);
            }
        }
    }
}
