using Cysharp.Threading.Tasks;
using Game.Runtime.Infrastructure.Configs;
using Game.Runtime.Infrastructure.LoadingTasks;
using UnityEngine.Scripting;

namespace Game.Runtime.Application.Initialization.LoadingTasks
{
    public class InitializeConfigsServiceTask : ILoadingTask
    {
        private readonly IConfigsService _configsService;
        private readonly ISpritesConfigService _spritesConfigService;
        private readonly IPlanetConfigService _planetConfigService;

        [Preserve]
        public InitializeConfigsServiceTask(IConfigsService configsService, ISpritesConfigService spritesConfigService,
            IPlanetConfigService planetConfigService)
        {
            _configsService = configsService;
            _spritesConfigService = spritesConfigService;
            _planetConfigService = planetConfigService;
        }

        public UniTask LoadAsync()
        {
            return UniTask.WhenAll(_spritesConfigService.Initialize(), _configsService.Initialize(),
                _planetConfigService.Initialize());
        }
    }
}