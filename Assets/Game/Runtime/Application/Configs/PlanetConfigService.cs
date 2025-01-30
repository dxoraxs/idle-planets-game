using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Runtime.Domain.Planet;
using Game.Runtime.Infrastructure.Configs;
using UnityEngine;

namespace Game.Runtime.Application.Configs
{
    [CreateAssetMenu(fileName = nameof(PlanetConfigService), menuName = "Game/" + nameof(PlanetConfigService),
        order = 0)]
    public class PlanetConfigService : ScriptableObject, IPlanetConfigService
    {
        [SerializeField] private List<PlanetConfig> _planetItems;

        public UniTask Initialize()
        {
            return UniTask.CompletedTask;
        }
        
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