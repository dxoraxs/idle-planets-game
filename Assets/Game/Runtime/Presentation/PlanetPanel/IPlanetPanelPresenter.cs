using System;

namespace Game.Runtime.Presentation.PlanetPanel
{
    public interface IPlanetPanelPresenter : IDisposable
    {
        event Action<string> OnPlanetSelected;
        event Action<bool> OnChangeResource;
        PlanetView GetPlanetById(string id);
        void OnClickUpgradeButton();
        void OnClickCloseButton();
    }
}