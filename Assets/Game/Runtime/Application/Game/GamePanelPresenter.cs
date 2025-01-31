using System;
using System.Collections.Generic;
using System.Linq;
using Game.Runtime.Application.Configs;
using Game.Runtime.Application.Planet;
using Game.Runtime.Domain.Planet;
using Game.Runtime.Infrastructure.Configs;
using Game.Runtime.Presentation.GamePanel;
using Game.Runtime.Presentation.GamePanel.Planet;
using VContainer;

namespace Game.Runtime.Application.Game
{
    public class GamePanelPresenter : IGamePanelPresenter
    {
        private readonly PlanetService _planetService;
        private readonly IConfigsService _configsService;
        private readonly ISpritesConfigService _spritesConfigService;
        
        [Preserve]
        public GamePanelPresenter(PlanetService planetService, IConfigsService configsService, 
            ISpritesConfigService spritesConfigService)
        {
            _planetService = planetService;
            _configsService = configsService;
            _spritesConfigService = spritesConfigService;
            
            foreach (var planet in _planetService.Planets.AllPlanets)
            {
                planet.Value.Opened += () => UpdateView(planet.Key);
                planet.Value.LevelUpped += () => UpdateView(planet.Key);
                planet.Value.IncomeCollected += () => UpdateView(planet.Key);
                planet.Value.IncomeAppeared += () => UpdateView(planet.Key);
            }
        }
        
        public event Action<string> PlanetClicked;
        public event Action<string> PlanetCoinClicked;
        public event Action<string> PlanetViewUpdated;

        public void OnClickPlanet(string id)
        {
            PlanetClicked?.Invoke(id);
            _planetService.OnPlanetClicked(id);
        }

        public void OnClickPlanetCoin(string id)
        {
            PlanetCoinClicked?.Invoke(id);
            _planetService.CollectIncome(id);
        }

        public IReadOnlyList<PlanetViewData> GetAllPlanets()
        {
            return _planetService.Planets.AllPlanets.Select(planet => GetPlanet(planet.Value)).ToList();
        }

        public PlanetViewData GetPlanet(string id)
        {
            var planet = _planetService.Planets.AllPlanets[id];
            return GetPlanet(planet);
        }

        private void UpdateView(string id)
        {
            PlanetViewUpdated?.Invoke(id);
        }

        private PlanetViewData GetPlanet(Domain.Planet.Planet planet)
        {
            var planetConfig = _configsService.Get<PlanetsConfigs>().GetPlanetConfig(planet.Id);

            var avatarName = planet.IsOpen ? planetConfig.IconName : planetConfig.LockIconName;
            var avatar = _spritesConfigService.GetSprite(avatarName);

            var planetViewData = new PlanetViewData(planet.Id, avatar, planet.IsOpen, planet.CostOpen,
                0, "1 sec");

            return planetViewData;
        }
    }
}