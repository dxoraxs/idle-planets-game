using System;

namespace Game.Runtime.Presentation.PlanetPanel
{
    public interface IPlanetPanelPresenter : IDisposable
    {
        event Action<string> OnPlanetSelected;
        event Action<bool> OnChangeResource;
        PlanetPanelData GetPlanetById(string id);
        void OnClickUpgradeButton();
        void OnClickCloseButton();
    }
}