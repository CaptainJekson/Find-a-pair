using CJ.FindAPair.Cards;
using Leopotam.Ecs;
using Zenject;

namespace CJ.FindAPair
{
    public class StartupInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var world = new EcsWorld();
            Container.BindInterfacesAndSelfTo<DefaultSystemsExecutor>().AsSingle().NonLazy();

            Container.BindInstance(world);

            CardsModule.Install(Container);
        }
    }

}

