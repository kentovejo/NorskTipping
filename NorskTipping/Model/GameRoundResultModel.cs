using Newtonsoft.Json;
using System.Collections.Generic;

namespace NorskTipping
{
    public class GameRoundResultModel
    {
        [JsonProperty("drawID")]
        public int DrawID { get; set; }
        [JsonProperty("drawDate")]
        public string DrawDate { get; set; }
        [JsonProperty("unsortedMainTable")]
        public List<int> UnsortedMainTable { get; set; }
        [JsonProperty("unsortedAddTable")]
        public List<int> UnsortedAddTable { get; set; }
        [JsonProperty("mainTable")]
        public List<int> MainTable { get; set; }
        [JsonProperty("addTable")]
        public List<int> AddTable { get; set; }
        [JsonProperty("prizeTable")]
        public List<string> PrizeTable { get; set; }
        [JsonProperty("prizeCaptionTable")]
        public List<string> PrizeCaptionTable { get; set; }
        [JsonProperty("winnerCountTable")]
        public List<int> WinnerCountTable { get; set; }
        [JsonProperty("turnover")]
        public int Turnover { get; set; }
        [JsonProperty("totalNumberOfWinners")]
        public int TotalNumberOfWinners { get; set; }
    }
}
