using System;
using System.Collections.Generic;

namespace Game.Runtime.Domain.Planet
{
    [Serializable]
    public struct PlanetSnapshot
    {
        public uint Level;
        public bool IsOpen;
        public ulong Income;
    }
}