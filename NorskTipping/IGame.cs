namespace NorskTipping
{
    public interface IGame
    {
        string Do(string path, int rounds, bool sorted, string filter);
    }
}
