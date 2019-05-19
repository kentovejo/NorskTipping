using System;

namespace NorskTipping
{
    public struct GameInit
    {
        public DateTime InitialDate { get; internal set; }
        public string Name { get; internal set; }
        public string EndPoint { get; internal set; }
    } 
}
