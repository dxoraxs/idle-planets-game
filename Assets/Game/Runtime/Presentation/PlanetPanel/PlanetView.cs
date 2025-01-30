using UnityEngine;

namespace Game.Runtime.Presentation.PlanetPanel
{
    public readonly struct PlanetView
    {
        public readonly string Name;
        public readonly Sprite Avatar;
        public readonly uint Population;
        public readonly uint Level;
        public readonly uint MaxLevel;
        public readonly uint Income;
        public readonly uint UpgradePrice;

        public PlanetView(string name, Sprite avatar, uint population, uint level,
            uint maxLevel, uint income, uint upgradePrice)
        {
            Name = name;
            Avatar = avatar;
            Population = population;
            Level = level;
            MaxLevel = maxLevel;
            Income = income;
            UpgradePrice = upgradePrice;
        }
    }
}