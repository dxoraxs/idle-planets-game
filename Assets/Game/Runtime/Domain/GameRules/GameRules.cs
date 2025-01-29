using Game.Runtime.Domain.Common;
using Game.Runtime.Domain.PlayerResources;

namespace Game.Runtime.Domain.GameRules
{
    public class GameRules
    {
        private readonly PlayerResources.PlayerResources _playerResources;

        public GameRules(PlayerResources.PlayerResources playerResources)
        {
            _playerResources = playerResources;
        }

        public bool CheckWinCondition(ulong requiredResources)
        {
            var resource = new Resource(Constants.Resources.SoftCurrency, requiredResources);
            return _playerResources.HasEnough(resource);
        }
    }
}