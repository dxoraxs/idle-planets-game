using UnityEngine;

namespace Game.Runtime.Presentation.GamePanel
{
    public struct PlanetViewData
    {
        public readonly string Id;
        public readonly Sprite Icon;
        public readonly bool IsOpen;
        public readonly bool IsIncomeReady;
        public readonly uint CostOpen;
        public readonly float ProgressBarValue;
        public readonly string ProgressText;

        public PlanetViewData(string id, Sprite icon, bool isOpen, bool isIncomeReady, uint costOpen, float progressBarValue, string progressText)
        {
            Id = id;
            Icon = icon;
            IsOpen = isOpen;
            IsIncomeReady = isIncomeReady;
            CostOpen = costOpen;
            ProgressBarValue = progressBarValue;
            ProgressText = progressText;
        }
    }
}