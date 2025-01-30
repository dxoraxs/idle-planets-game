using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime.Domain.Planet
{
    [Serializable]
    public class PlanetsConfigs
    {
        [SerializeField] private List<PlanetConfig> _planetItems;
        
        public PlanetConfig GetPlanetConfig(string id)
        {
            foreach (var config in _planetItems)
            {
                if (config.Id == id)
                {
                    return config;
                }
            }

            throw new ArgumentException($"Config for planet id {id} is not exists");
        }
        
        public IReadOnlyList<PlanetConfig> Planets()
        {
            return _planetItems;
        }
    }
}