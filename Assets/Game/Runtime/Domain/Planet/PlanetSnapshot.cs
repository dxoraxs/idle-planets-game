using System;
using System.Collections.Generic;

namespace Game.Runtime.Domain.Planet
{
    [Serializable]
    public struct PlanetSnapshot
    {
        public string Name;
        public uint Level;
        public bool IsOpen;
        public ulong Income;
    }
}