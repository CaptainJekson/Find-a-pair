using CJ.FindAPair.Cards.Configs;
using CJ.FindAPair.Cards.Systems;
using Zenject;

namespace CJ.FindAPair.Cards
{
    public class CardsModule : Installer<CardsModule>
    {
        public override void InstallBindings()
        {
            Container.Bind<LevelConfigGroup>().FromScriptableObjectResource("LevelConfigGroup").AsSingle();

            Container.Bind<CreateCardSystem>().AsSingle();
            Container.Bind<CardOpeningSystem>().AsSingle();

            Container.BindInterfacesAndSelfTo<CardsSystemsExecutor>().AsSingle().NonLazy();
        }
    }
}

