using CJ.FindAPair.Cards.Systems;
using Zenject;

namespace CJ.FindAPair.Cards
{
    public class CardsModule : Installer<CardsModule>
    {
        public override void InstallBindings()
        {
            Container.Bind<CreateCardSystem>().AsSingle();

            Container.BindInterfacesAndSelfTo<CardsSystemsExecutor>().AsSingle().NonLazy();
        }
    }
}

