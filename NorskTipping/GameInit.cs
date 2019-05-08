using System;

namespace NorskTipping
{
    public struct GameInit
    {
        public DateTime InitialDate { get; internal set; }
        public string Name { get; internal set; }
        public string EndPoint { get; internal set; }
        public int CurrentRound => (int)Math.Floor((DateTime.Today.Subtract(InitialDate).TotalDays - 1) / 7) + 1;
    } 
}
