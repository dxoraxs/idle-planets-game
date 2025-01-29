using Game.Runtime.Domain.Common;
using Game.Runtime.Domain.GameRules;
using Game.Runtime.Domain.Planet;
using Game.Runtime.Domain.PlayerResources;

namespace Game.Runtime.Application.PlanetService
{
    public class PlanetService
    {
        private readonly GameRules _gameRules;
        private readonly PlayerResources _playerResources;
            
        public void BuyPlanet(Planet planet)
        {
            var requiredResources = planet.GetCurrentUpgrade().UpgradeCost;
            if (_gameRules.CheckWinCondition(requiredResources))
            {
                var resource = new Resource(Constants.Resources.SoftCurrency, requiredResources);
                _playerResources.Remove(resource);
                planet.SetOpen();
                //_planetRepository.SavePlanets();
            }
        }

        public void UpgradePlanet(Planet planet)
        {
            var requiredResources = planet.GetCurrentUpgrade().UpgradeCost;
            if (_gameRules.CheckWinCondition(requiredResources))
            {
                var resource = new Resource(Constants.Resources.SoftCurrency, requiredResources);
                _playerResources.Remove(resource);
                planet.LevelUp();
                //_planetRepository.SavePlanets();
            }
        }

        public void CollectIncome(Planet planet)
        {
            var income = planet.IncomeValue;
            planet.CollectIncome();
            var resource = new Resource(Constants.Resources.SoftCurrency, income);
            _playerResources.Add(resource);
        }
        
        public PlanetInfo GetPlanetInfo(Planet planet)
        {
            var currentUpgrade = planet.GetCurrentUpgrade();
            return new PlanetInfo
            {
                Name = planet.Name,
                Population = planet.Population,
                Income = currentUpgrade.Income,
                Level = planet.Level,
                UpgradeCost = currentUpgrade.UpgradeCost
            };
        }
    }
}