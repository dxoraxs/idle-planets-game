using System;
using Game.Runtime.Application.Resources;
using Game.Runtime.Domain.Common;
using Game.Runtime.Domain.GameRules;
using Game.Runtime.Domain.Planet;
using Game.Runtime.Domain.PlayerResources;
using Game.Runtime.Infrastructure.Configs;
using Game.Runtime.Infrastructure.Repository;
using Game.Runtime.Presentation.GamePanel.Planet;
using UnityEngine.Scripting;

namespace Game.Runtime.Application.Planet
{
    public class PlanetService : ISaveable
    {
        private readonly GameRules _gameRules;
        private readonly PlayerResourcesController _playerResourcesController;
        private readonly IRepositoryService _repositoryService;
        private readonly IConfigsService _configsService;

        public Planets Planets { get; private set; }
        public event Action<string> PlanetUpgraded;

        [Preserve]
        public PlanetService(IRepositoryService repositoryService, IConfigsService configsService, GameRules gameRules,
            PlayerResourcesController playerResourcesController)
        {
            _repositoryService = repositoryService;
            _configsService = configsService;
            _gameRules = gameRules;
            _playerResourcesController = playerResourcesController;
        }

        public void Initialize()
        {
            Planets = new Planets();
            var planets = _configsService.Get<PlanetsConfigs>().Planets();
            Planets.LoadFromConfig(planets);
            
            if (_repositoryService.TryLoad<AllPlanetSnapshots>(out var snapshot))
            {
                Planets.RestoreFromSnapshot(snapshot);
            }
        }

        public void OnPlanetClicked(string id)
        {
            if (!Planets.AllPlanets[id].IsOpen)
            {
                BuyPlanet(id);
            }
            else
            {
                PlanetUpgraded?.Invoke(id);
            }
        }

        private void BuyPlanet(string id)
        {
            var planet = Planets.AllPlanets[id];
            var requiredResources = planet.CostOpen;
            if (_gameRules.CheckPayCondition(requiredResources))
            {
                var resource = new Resource(Constants.Resources.SoftCurrency, requiredResources);
                _playerResourcesController.PlayerResources.Remove(resource);
                planet.SetOpen();
                //_planetRepository.SavePlanets();
            }
        }

        public void UpgradePlanet(string id)
        {
            var planet = Planets.AllPlanets[id];
            var requiredResources = planet.GetCurrentUpgrade().Cost;
            if (_gameRules.CheckPayCondition(requiredResources))
            {
                var resource = new Resource(Constants.Resources.SoftCurrency, requiredResources);
                _playerResourcesController.PlayerResources.Remove(resource);
                planet.LevelUp();
                //_planetRepository.SavePlanets();
            }
        }

        public void CollectIncome(string id)
        {
            var planet = Planets.AllPlanets[id];
            planet.CollectIncome();
            var planetConfig = _configsService.Get<PlanetsConfigs>().GetPlanetConfig(id);
            var income = planetConfig.UpgradeConfigs[planet.Level].Income;
            var resource = new Resource(Constants.Resources.SoftCurrency, income);
            _playerResourcesController.PlayerResources.Add(resource);
        }
        
        public void Save()
        {
            _repositoryService.Save(Planets.GetSnapshot());
        }
    }
}