using System;
using UnityEngine;

namespace Game.Runtime.Domain.Planet
{
    [Serializable]
    public class PlanetConfig
    {
        [field:SerializeField] public string Id { get; private set; }
        [field:SerializeField] public string Name { get; private set; }
        [field:SerializeField] public string IconName { get; private set; }
        [field:SerializeField] public string LockIconName { get; private set; }
        [field:SerializeField] public uint ConstOpen { get; private set; }
        [field:SerializeField] public float TimerPerTick { get; private set; }
        [field:SerializeField] public uint Population { get; private set; }
        [field:SerializeField] public PlanetUpgradeConfig[] UpgradeConfigs { get; private set; }
    }
}