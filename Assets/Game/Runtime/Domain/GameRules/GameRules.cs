﻿using Game.Runtime.Application.Resources;
using Game.Runtime.Domain.Common;
using Game.Runtime.Domain.PlayerResources;

namespace Game.Runtime.Domain.GameRules
{
    public class GameRules
    {
        private readonly PlayerResourcesController _playerResourcesController;

        public GameRules(PlayerResourcesController playerResourcesController)
        {
            _playerResourcesController = playerResourcesController;
        }

        public bool CheckPayCondition(ulong requiredResources)
        {
            var resource = new Resource(Constants.Resources.SoftCurrency, requiredResources);
            return _playerResourcesController.PlayerResources.HasEnough(resource);
        }
    }
}