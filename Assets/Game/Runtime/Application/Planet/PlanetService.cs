using System;
using Game.Runtime.Application.Resources;
using Game.Runtime.Domain.Common;
using Game.Runtime.Domain.GameRules;
using Game.Runtime.Domain.Planet;
using Game.Runtime.Domain.PlayerResources;
using Game.Runtime.Infrastructure.Configs;
using Game.Runtime.Infrastructure.Repository;
using Game.Runtime.Infrastructure.Time;
using UnityEngine.Scripting;

namespace Game.Runtime.Application.Planet
{
    public class PlanetService : ISaveable
    {
        private readonly GameRules _gameRules;
        private readonly PlayerResourcesController _playerResourcesController;
        private readonly IRepositoryService _repositoryService;
        private readonly IConfigsService _configsService;
        private readonly ITimeService _timeService;

        public Planets Planets { get; private set; }
        public event Action<string> PlanetUpgraded;

        [Preserve]
        public PlanetService(IRepositoryService repositoryService, IConfigsService configsService, GameRules gameRules,
            PlayerResourcesController playerResourcesController, ITimeService timeService)
        {
            _repositoryService = repositoryService;
            _configsService = configsService;
            _gameRules = gameRules;
            _playerResourcesController = playerResourcesController;
            _timeService = timeService;
        }

        public void Initialize()
        {
            Planets = new Planets();
            var planets = _configsService.Get<PlanetsConfigs>().Planets();
            Planets.LoadFromConfig(planets);

            foreach (var planet in Planets.AllPlanets)
            {
                planet.Value.Opened += () => StartTimer(planet.Key);
                planet.Value.IncomeCollected += () => StartTimer(planet.Key);
            }
            
            if (_repositoryService.TryLoad<AllPlanetSnapshots>(out var snapshot))
            {
                Planets.RestoreFromSnapshot(snapshot);
                foreach (var planet in Planets.AllPlanets)
                {
                    if (planet.Value.IsTimerEnable)
                    {
                        StartTimer(planet.Key);
                    }
                }
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

        public void CollectIncome(string id)
        {
            var planet = Planets.AllPlanets[id];
            planet.CollectIncome();
            var planetConfig = _configsService.Get<PlanetsConfigs>().GetPlanetConfig(id);
            var currentUpgradeConfig = planetConfig.UpgradeConfigs[planet.Level];
            var income = currentUpgradeConfig.Income;
            var resource = new Resource(Constants.Resources.SoftCurrency, income);
            _playerResourcesController.PlayerResources.Add(resource);
        }

        public void Save()
        {
            _repositoryService.Save(Planets.GetSnapshot());
        }

        public void UpgradePlanet(string id)
        {
            var planet = Planets.AllPlanets[id];
            var requiredResources = planet.GetCurrentUpgrade().Cost;
            if (_gameRules.CheckPayCondition(requiredResources))
            {
                var resource = new Resource(Constants.Resources.SoftCurrency, requiredResources);
                planet.LevelUp();
                _playerResourcesController.PlayerResources.Remove(resource);
            }
        }

        private void StartTimer(string id)
        {
            var planet = Planets.AllPlanets[id];
            
            if (_timeService.AddAndGetTimer(id, planet.IncomeTimer, out var timer))
            {
                planet.StartTimer();
                timer.Ended += planet.GenerateIncome;
                timer.Ticked += planet.SetIncomeTimer;
            }
        }

        private void BuyPlanet(string id)
        {
            var planet = Planets.AllPlanets[id];
            var requiredResources = _configsService.Get<PlanetsConfigs>().GetPlanetConfig(id).CostOpen;
            
            if (_gameRules.CheckPayCondition(requiredResources))
            {
                var resource = new Resource(Constants.Resources.SoftCurrency, requiredResources);
                _playerResourcesController.PlayerResources.Remove(resource);
                planet.SetOpen();
            }
        }
    }
}