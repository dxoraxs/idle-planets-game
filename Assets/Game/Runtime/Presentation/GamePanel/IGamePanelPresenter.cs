using System;
using System.Collections.Generic;

namespace Game.Runtime.Presentation.GamePanel.Planet
{
    public interface IGamePanelPresenter
    {
        event Action<string> PlanetClicked;
        event Action<string> PlanetCoinClicked;
        event Action<string> PlanetViewUpdated;
        void OnClickPlanet(string id);
        void OnClickPlanetCoin(string id);
        IReadOnlyList<PlanetViewData> GetAllPlanets();
        PlanetViewData GetPlanet(string id);
    }
}