
using Game.Runtime.Domain.PlayerResources;

namespace Game.Runtime.Domain.GameRules
{
    public class GameRules
    {
        private readonly string _resourceId;
        private readonly PlayerResources.PlayerResources _playerResources;

        public GameRules(string resourceId, PlayerResources.PlayerResources playerResources)
        {
            _resourceId = resourceId;
            _playerResources = playerResources;
        }

        public bool CheckWinCondition(ulong requiredResources)
        {
            var resource = new Resource(_resourceId, requiredResources);
            return _playerResources.HasEnough(resource);
        }
    }
}