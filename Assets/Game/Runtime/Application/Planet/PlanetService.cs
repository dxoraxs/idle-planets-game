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
        private readonly PlayerResources _playerResources;
        private readonly IRepositoryService _repositoryService;
        private readonly IConfigsService _configsService;

        public Planets Planets { get; private set; }

        [Preserve]
        public PlanetService(IRepositoryService repositoryService, IConfigsService configsService)
        {
            _repositoryService = repositoryService;
            _configsService = configsService;
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
        }

        private void BuyPlanet(string id)
        {
            var planet = Planets.AllPlanets[id];
            var requiredResources = planet.GetCurrentUpgrade().Cost;
            if (_gameRules.CheckWinCondition(requiredResources))
            {
                var resource = new Resource(Constants.Resources.SoftCurrency, requiredResources);
                _playerResources.Remove(resource);
                planet.SetOpen();
                //_planetRepository.SavePlanets();
            }
        }

        public void UpgradePlanet(Domain.Planet.Planet planet)
        {
            var requiredResources = planet.GetCurrentUpgrade().Cost;
            if (_gameRules.CheckWinCondition(requiredResources))
            {
                var resource = new Resource(Constants.Resources.SoftCurrency, requiredResources);
                _playerResources.Remove(resource);
                planet.LevelUp();
                //_planetRepository.SavePlanets();
            }
        }

        public void CollectIncome(string id)
        {
            var planet = Planets.AllPlanets[id];
            var income = planet.IncomeValue;
            planet.CollectIncome();
            var resource = new Resource(Constants.Resources.SoftCurrency, income);
            _playerResources.Add(resource);
        }
        
        public void Save()
        {
            _repositoryService.Save(Planets.GetSnapshot());
        }
    }
}