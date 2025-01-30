using System.Collections.Generic;
using System.Linq;
using Game.Runtime.Domain.Common;

namespace Game.Runtime.Domain.Planet
{
    public class Planets : ISnapshotable<AllPlanetSnapshots>
    {
        private readonly Dictionary<string, Planet> _planets = new();

        public IReadOnlyDictionary<string, Planet> AllPlanets => _planets;
        
        public void LoadFromConfig(IReadOnlyList<PlanetConfig> planetConfigs)
        {
            foreach (var planetConfig in planetConfigs)
            {
                var planetUpgrade = planetConfig.UpgradeConfigs.Select(planetUpgrade =>
                    new PlanetUpgrade(planetUpgrade.Income, planetUpgrade.Cost)).ToArray();
                var newPlanet = new Planet(planetConfig.Id, planetConfig.Name, planetConfig.ConstOpen,
                    planetConfig.TimerPerTick, planetConfig.Population, planetUpgrade);
                _planets.Add(planetConfig.Id, newPlanet);
            }
        }

        public AllPlanetSnapshots GetSnapshot()
        {
            var result = new AllPlanetSnapshots();
            foreach (var planet in _planets)
            {
                result.Planets.Add(planet.Key, planet.Value.GetSnapshot());
            }

            return result;
        }

        public void RestoreFromSnapshot(AllPlanetSnapshots snapshot)
        {
            foreach (var planetSnapshot in snapshot.Planets)
            {
                var newPlanet = _planets[planetSnapshot.Key];
                newPlanet.RestoreFromSnapshot(planetSnapshot.Value);
            }
        }
    }
}