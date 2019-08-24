using System;
using System.Collections.Generic;

namespace NorskTipping
{
    public class BasicGame
    {
        public GameModel Init;
        public int CurrentRound => (int)Math.Floor((DateTime.Today.Subtract(Init.InitialDate).TotalDays - 1) / 7) + 1;
        public GameRoundResultModel NextRoundEstimate { get; set; } = new GameRoundResultModel();
        public List<GameResultModel> Model = new List<GameResultModel>();
        public static GameRoundResultModel GetNumbers(string path, int round)
        {            
            return new GameRoundResult().GetRound(path, round);
        }
        public void ConvertToModel(string path, IEnumerable<int> labels, bool sorted)
        {
            // Add estimation
            if (NextRoundEstimate.UnsortedMainTable != null)
            {
                if (NextRoundEstimate.UnsortedMainTable.Count > 0)
                {
                    for(var i = NextRoundEstimate.UnsortedMainTable.Count; i < 7; i++)
                        NextRoundEstimate.UnsortedMainTable.Add(0);
                    if (sorted)
                        NextRoundEstimate.UnsortedMainTable.Sort();
                    for (var j = 0; j < NextRoundEstimate.UnsortedMainTable.Count; j++)
                        Model[j].Data.Add(NextRoundEstimate.UnsortedMainTable[j]);
                }
            }
            foreach (var i in labels)
            {
                var res = GetNumbers(path + Init.Name, i);
                if(res.UnsortedMainTable == null)
                    continue;
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
