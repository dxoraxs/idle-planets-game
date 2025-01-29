using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Runtime.Domain.Planet;

namespace Game.Runtime.Infrastructure.Configs
{
    public interface IPlanetConfigService
    {
        UniTask Initialize();
        IReadOnlyList<PlanetConfig> Planets();
    }
}