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

        public event Action TimerUpdated;

        public readonly string Id;
        public readonly float TimerPerTick;
        public bool IsIncomeReady { get; private set; }
        public uint Level { get; private set; }
        public bool IsOpen { get; private set; }
        public bool IsTimerEnable { get; private set; }
        public float IncomeTimer { get; private set; }
        private readonly PlanetUpgrade[] _upgrades;

        public Planet(string id, float timerPerTick, PlanetUpgrade[] upgrades)
        {
            Id = id;
            IncomeTimer = TimerPerTick = timerPerTick;
            _upgrades = upgrades;
        }

        public void SetIncomeTimer(float timer)
        {
            IncomeTimer = timer;
            TimerUpdated?.Invoke();
        }

        public PlanetUpgrade GetCurrentUpgrade()
        {
            return _upgrades[Level];
        }

        public void SetOpen()
        {
            IsOpen = true;
            Opened?.Invoke();
        }

        public void LevelUp()
        {
            Level++;
            LevelUpped?.Invoke();
        }

        public void StartTimer()
        {
            IsTimerEnable = true;
        }

        public void GenerateIncome()
        {
            IncomeTimer = TimerPerTick;
            IsTimerEnable = false;
            IsIncomeReady = true;
            IncomeAppeared?.Invoke();
        }

        public void CollectIncome()
        {
            IsIncomeReady = false;
            IncomeCollected?.Invoke();
        }

        public PlanetSnapshot GetSnapshot()
        {
            return new PlanetSnapshot(Level, IsOpen, IsIncomeReady, IsTimerEnable, IncomeTimer);
        }

        public void RestoreFromSnapshot(PlanetSnapshot snapshot)
        {
            Level = snapshot.Level;
            IsOpen = snapshot.IsOpen;
            IsIncomeReady = snapshot.Income;
            IncomeTimer = snapshot.IncomeTimer;
            IsTimerEnable = snapshot.IsTimerEnable;
        }
    }
}