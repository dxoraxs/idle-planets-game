using System;

namespace Game.Runtime.Domain.Planet
{
    [Serializable]
    public class PlanetUpgrade
    {
        public readonly uint Income;
        public readonly uint Cost;

        public PlanetUpgrade(uint income, uint cost)
        {
            Income = income;
            Cost = cost;
        }
    }
}