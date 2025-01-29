using Game.Runtime.Domain.Common;
using Game.Runtime.Domain.GameRules;
using Game.Runtime.Domain.Planet;
using Game.Runtime.Domain.PlayerResources;

namespace Game.Runtime.Application.Planet
{
    public class PlanetService
    {
        private readonly GameRules _gameRules;
        private readonly PlayerResources _playerResources;
        
        public void BuyPlanet(Domain.Planet.Planet planet)
        {
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

        public void CollectIncome(Domain.Planet.Planet planet)
        {
            var income = planet.IncomeValue;
            planet.CollectIncome();
            var resource = new Resource(Constants.Resources.SoftCurrency, income);
            _playerResources.Add(resource);
        }
        
        public PlanetInfo GetPlanetInfo(Domain.Planet.Planet planet)
        {
            var currentUpgrade = planet.GetCurrentUpgrade();
            return new PlanetInfo
            {
                Name = planet.Name,
                Population = planet.Population,
                Income = currentUpgrade.Income,
                Level = planet.Level,
                UpgradeCost = currentUpgrade.Cost
            };
        }
    }
}