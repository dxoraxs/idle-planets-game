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

        public IReadOnlyList<PlanetConfig> Planets()
        {
            return _planetItems;
        }
    }
}