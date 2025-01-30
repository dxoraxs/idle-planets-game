using Game.Runtime.Application.Game;
using Game.Runtime.Application.Planet;
using Game.Runtime.Application.Resources;
using Game.Runtime.Application.SaveGame;
using Game.Runtime.Infrastructure.Factories;
using Game.Runtime.Infrastructure.Panels;
using Game.Runtime.Infrastructure.Repository;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Runtime.Application.DI
{
    public class MainSceneLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private PanelsService _panelsService;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IIocFactory, VContainerFactory>(Lifetime.Scoped);
            builder.RegisterEntryPoint<GameSaveController>(Lifetime.Scoped);
            builder.RegisterEntryPoint<GameController>(Lifetime.Scoped).AsSelf();
            builder.RegisterInstance<IPanelsService>(InstantiatePanelsService());

            ConfigureDomainControllers(builder);
        }

        private void ConfigureDomainControllers(IContainerBuilder builder)
        {
            builder.Register<PlayerResourcesController>(Lifetime.Scoped).AsSelf().As<ISaveable>();
            builder.Register<PlanetService>(Lifetime.Scoped).AsSelf().As<ISaveable>();
            //builder.RegisterEntryPoint<PlanetService>().AsSelf();
        }
        
        private IPanelsService InstantiatePanelsService()
        {
            var service = Instantiate(_panelsService);
            DontDestroyOnLoad(service.gameObject);

            return service;
        }
    }
}
