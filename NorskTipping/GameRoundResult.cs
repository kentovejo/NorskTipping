using Newtonsoft.Json;

namespace NorskTipping
{
    class GameRoundResult : IGameResult
    {
        public GameRoundResultModel GetRound(string path, int round)
        {
            var json = FileRepository.GetRoundResults(path, round);
            return !string.IsNullOrEmpty(json) ? JsonConvert.DeserializeObject<GameRoundResultModel>(json) : new GameRoundResultModel();
        }
    }
}
