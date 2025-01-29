using System;
using UnityEngine;

namespace Game.Runtime.Domain.Planet
{
    [Serializable]
    public class PlanetUpgradeConfig
    {
        [field:SerializeField] public uint Income { get; private set; }
        [field:SerializeField] public uint Cost { get; private set; }
    }
}