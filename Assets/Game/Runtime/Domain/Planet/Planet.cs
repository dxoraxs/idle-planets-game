using System;
using Game.Runtime.Domain.Common;

namespace Game.Runtime.Domain.Planet
{
    [Serializable]
    public class Planet : ISnapshotable<PlanetSnapshot>
    {
        public event Action Opened;

        public event Action LevelUpped;

        public event Action IncomeCollected;

        public event Action IncomeAppeared;

        public readonly string Name;
        public readonly uint CostOpen;
        public readonly float TimerPerTick;
        public readonly uint Population;
        public ulong IncomeValue { get; private set; }
        public uint Level { get; private set; }
        private bool _isOpen;
        private readonly PlanetUpgrade[] _upgrades;
        
        public Planet(string name, uint costOpen, float timerPerTick, uint population, PlanetUpgrade[] upgrades)
        {
            Name = name;
            CostOpen = costOpen;
            TimerPerTick = timerPerTick;
            Population = population;
            _upgrades = upgrades;
        }

        public PlanetUpgrade GetCurrentUpgrade()
        {
            return _upgrades[Level];
        }

        public void SetOpen()
        {
            _isOpen = true;
            Opened?.Invoke();
        }

        public void LevelUp()
        {
            Level++;
            LevelUpped?.Invoke();
        }

        public void GenerateIncome()
        {
            IncomeValue += GetCurrentUpgrade().Income;
            IncomeAppeared?.Invoke();
        }

        public void CollectIncome()
        {
            IncomeValue = 0;
            IncomeCollected?.Invoke();
        }

        public PlanetSnapshot GetSnapshot()
        {
            return new PlanetSnapshot { Name = Name, Level = Level, IsOpen = _isOpen, Income = IncomeValue };
        }

        public void RestoreFromSnapshot(PlanetSnapshot snapshot)
        {
            Level = snapshot.Level;
            _isOpen = snapshot.IsOpen;
            IncomeValue = snapshot.Income;
        }
    }
}