using CJ.FindAPair.UI;
using Zenject;

namespace CJ.FindAPair.Modules.UI.Installer
{
    public class UIInstaller : Installer<UIInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<UISceneLoader>().AsSingle();
        }
    }
}