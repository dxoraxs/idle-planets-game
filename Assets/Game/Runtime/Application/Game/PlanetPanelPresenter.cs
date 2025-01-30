using System;
using Game.Runtime.Application.Configs;
using VContainer;
using Game.Runtime.Application.Resources;
using Game.Runtime.Domain.Common;
using Game.Runtime.Domain.Planet;
using Game.Runtime.Domain.PlayerResources;
using Game.Runtime.Infrastructure.Configs;
using Game.Runtime.Presentation.PlanetPanel;

namespace Game.Runtime.Application.Game
{
    public class PlanetPanelPresenter : IPlanetPanelPresenter
    {
        private readonly PlayerResourcesController _playerResourcesController;
        private readonly IConfigsService _configsService;
        private readonly Planets _planets;

        private readonly ISpritesConfigService _spritesConfigService;
        private string _currentPlanetId;

        public event Action<string> OnPlanetSelected;
        public event Action<bool> OnChangeResource;

        [Preserve]
        public PlanetPanelPresenter(PlayerResourcesController playerResourcesController, Planets planets,
            IConfigsService configsService, ISpritesConfigService spritesConfigService)
        {
            _playerResourcesController = playerResourcesController;
            _configsService = configsService;
            _spritesConfigService = spritesConfigService;
            _planets = planets;

            _playerResourcesController.PlayerResources.ResourceCountAdded += OnResourceCountChanged;
            _playerResourcesController.PlayerResources.ResourceCountRemoved += OnResourceCountChanged;
        }

        private void OnResourceCountChanged(string resourceId, ulong changedCount, ulong totalCount)
        {
            if (resourceId == Constants.Resources.SoftCurrency)
            {
                var planet = _planets.AllPlanets[_currentPlanetId];
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

        public PlanetView GetPlanetById(string id)
        {
            var planet = _planets.AllPlanets[_currentPlanetId];

            var planetConfig = _configsService.Get<PlanetConfigService>().GetPlanetConfig(id);

            var avatarName = planet.IsOpen ? planetConfig.IconName : planetConfig.LockIconName;
            var avatar = _spritesConfigService.GetSprite(avatarName);

            var currentUpgrade = planet.GetCurrentUpgrade();
            
            var planetView = new PlanetView(planetConfig.Name, avatar, planetConfig.Population, planet.Level, 
                (uint)planetConfig.UpgradeConfigs.Length + 1, currentUpgrade.Income, currentUpgrade.Cost);

            return planetView;
        }

        public void OnClickUpgradeButton()
        {
            var planet = _planets.AllPlanets[_currentPlanetId];
            var currentUpgrade = planet.GetCurrentUpgrade();
            var upgradeCost = currentUpgrade.Cost;
            
            _playerResourcesController.PlayerResources.Remove(new Resource(Constants.Resources.SoftCurrency, upgradeCost));
            planet.LevelUp();
        }

        public void OnClickCloseButton()
        {
        }
    }
}