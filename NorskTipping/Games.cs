using System.Collections.Generic;

namespace NorskTipping
{
    public class Games
    {
        public readonly IList<IGame> GameTypes = new List<IGame>();

        public Games()
        {
            GameTypes.Add(new Lotto());
            GameTypes.Add(new Vikinglotto());
            GameTypes.Add(new EuroJackpot());
        }

        public string Do(int index, string path, int rounds, bool sorted, string filter)
        {
            return GameTypes[index].Do(path, rounds, sorted, filter);
        }
    } 
}
