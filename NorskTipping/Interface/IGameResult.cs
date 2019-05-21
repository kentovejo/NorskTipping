namespace NorskTipping
{
    public interface IGameResult
    {
        GameRoundResultModel GetRound(string path, int round);
    }
}
