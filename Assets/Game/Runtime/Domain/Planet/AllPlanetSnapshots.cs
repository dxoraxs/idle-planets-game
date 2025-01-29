using System;
using System.Collections.Generic;

namespace Game.Runtime.Domain.Planet
{
    [Serializable]
    public struct AllPlanetSnapshots
    {
        public Dictionary<string,PlanetSnapshot> Planets;
    }
}