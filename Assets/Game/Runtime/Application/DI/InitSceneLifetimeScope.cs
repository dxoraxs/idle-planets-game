using Game.Runtime.Application.Initialization;
using Game.Runtime.Infrastructure.Factories;
using VContainer;
using VContainer.Unity;

namespace Game.Runtime.Application.DI
{
    public class InitSceneLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IIocFactory, VContainerFactory>(Lifetime.Scoped);
            builder.RegisterEntryPoint<InitializationController>();
        }
    }
}
