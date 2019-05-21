using Newtonsoft.Json;

namespace NorskTipping
{
    class GameRoundResult : IGameResult
    {
        public GameRoundResultModel GetRound(string path, int round)
        {
            var json = ResultsRepository.GetRoundResults(path, round);
            if (!string.IsNullOrEmpty(json))
                return JsonConvert.DeserializeObject<GameRoundResultModel>(json);
            return new GameRoundResultModel();
        }
    }
}
