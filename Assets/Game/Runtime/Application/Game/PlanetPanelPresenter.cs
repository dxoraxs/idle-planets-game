using System;
using Game.Runtime.Application.Configs;
using Game.Runtime.Application.Planet;
using VContainer;
using Game.Runtime.Application.Resources;
using Game.Runtime.Domain.Common;
using Game.Runtime.Domain.Planet;
using Game.Runtime.Domain.PlayerResources;
using Game.Runtime.Infrastructure.Configs;
using Game.Runtime.Infrastructure.Panels;
using Game.Runtime.Presentation.GamePanel.Planet;
using Game.Runtime.Presentation.PlanetPanel;

namespace Game.Runtime.Application.Game
{
    public class PlanetPanelPresenter : IPlanetPanelPresenter
    {
        private readonly PlayerResourcesController _playerResourcesController;
        private readonly IConfigsService _configsService;
        private readonly PlanetService _planetService;
        private readonly IPanelsService _panelsService;
        private readonly ISpritesConfigService _spritesConfigService;
        private string _currentPlanetId;

        public event Action<string> OnPlanetSelected;
        public event Action<bool> OnChangeResource;

        [Preserve]
        public PlanetPanelPresenter(PlayerResourcesController playerResourcesController, PlanetService planetService,
            IConfigsService configsService, ISpritesConfigService spritesConfigService, IPanelsService panelsService,
            IGamePanelPresenter gamePanelPresenter)
        {
            _playerResourcesController = playerResourcesController;
            _configsService = configsService;
            _spritesConfigService = spritesConfigService;
            _planetService = planetService;
            _panelsService = panelsService;

            _playerResourcesController.PlayerResources.ResourceCountAdded += OnResourceCountChanged;
            _playerResourcesController.PlayerResources.ResourceCountRemoved += OnResourceCountChanged;
            gamePanelPresenter.PlanetClicked += PlanetClicked;
            
            var panel = _panelsService.Open<PlanetPanel>();
            panel.SetPresenter(this);
            panel.Hide();
        }

        private void OnResourceCountChanged(string resourceId, ulong changedCount, ulong totalCount)
        {
            if (resourceId == Constants.Resources.SoftCurrency)
            {
                var planet = _planetService.Planets.AllPlanets[_currentPlanetId];
                var currentPlanetUpdate = planet.GetCurrentUpgrade();
                var isHaveResource = totalCount >= currentPlanetUpdate.Cost;
                OnChangeResource?.Invoke(isHaveResource);
            }
        }

        public void Dispose()
        {
            _playerResourcesController.PlayerResources.ResourceCountAdded -= OnResourceCountChanged;
            _playerResourcesController.PlayerResources.ResourceCountRemoved -= OnResourceCountChanged;
        }

        public PlanetPanelData GetPlanetById(string id)
        {
            var planet = _planetService.Planets.AllPlanets[_currentPlanetId];

            var planetConfig = _configsService.Get<PlanetsConfigs>().GetPlanetConfig(id);

            var avatarName = planet.IsOpen ? planetConfig.IconName : planetConfig.LockIconName;
            var avatar = _spritesConfigService.GetSprite(avatarName);

            var currentUpgrade = planet.GetCurrentUpgrade();
            
            var planetView = new PlanetPanelData(planetConfig.Name, avatar, planetConfig.Population, planet.Level, 
                (uint)planetConfig.UpgradeConfigs.Length + 1, currentUpgrade.Income, currentUpgrade.Cost);

            return planetView;
        }

        public void OnClickUpgradeButton()
        {
            var planet = _planetService.Planets.AllPlanets[_currentPlanetId];
            var currentUpgrade = planet.GetCurrentUpgrade();
            var upgradeCost = currentUpgrade.Cost;

            var costResource = new Resource(Constants.Resources.SoftCurrency, upgradeCost);
            _playerResourcesController.PlayerResources.Remove(costResource);
            planet.LevelUp();
        }

        public void OnClickCloseButton()
        {
            _panelsService.Close<PlanetPanel>();
        }

        public void PlanetClicked(string id)
        {
            var planet = _planetService.Planets.AllPlanets[_currentPlanetId];
            if (planet.IsOpen)
            {
                _panelsService.Open<PlanetPanel>();
                OnPlanetSelected?.Invoke(id);
            }
        }
    }
}