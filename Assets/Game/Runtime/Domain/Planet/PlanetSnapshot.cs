using System;

namespace Game.Runtime.Domain.Planet
{
    [Serializable]
    public struct PlanetSnapshot
    {
        public readonly uint Level;
        public readonly bool IsOpen;
        public readonly bool Income;
        public readonly bool IsTimerEnable;
        public readonly float IncomeTimer;

        public PlanetSnapshot(uint level, bool isOpen, bool income, bool isTimerEnable, float incomeTimer)
        {
            Level = level;
            IsOpen = isOpen;
            Income = income;
            IsTimerEnable = isTimerEnable;
            IncomeTimer = incomeTimer;
        }
    }
}