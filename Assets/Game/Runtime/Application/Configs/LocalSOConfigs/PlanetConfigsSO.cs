using Game.Runtime.Domain.Planet;
using UnityEngine;

namespace Game.Runtime.Application.Configs.LocalSOConfigs
{
    [CreateAssetMenu(fileName = nameof(PlanetConfigsSO), menuName = "Game/Configs/" + nameof(PlanetConfigsSO),
        order = 0)]
    public class PlanetConfigsSO : ScriptableObject
    {
        public PlanetsConfigs PlanetsConfigs;
    }
}