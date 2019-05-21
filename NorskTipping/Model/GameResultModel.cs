using System.Collections;
using Newtonsoft.Json;

namespace NorskTipping
{
    public class GameResultModel
    {
        [JsonProperty(PropertyName = "data")]
        public ArrayList Data { get; set; } = new ArrayList();
        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; }
        [JsonProperty(PropertyName = "borderColor")]
        public string BorderColor { get; set; }
        [JsonProperty(PropertyName = "fill")]
        public bool Fill { get; set; }
    }
}
