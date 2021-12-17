using CJ.FindAPair.Modules.CoreGames.Installer;
using CJ.FindAPair.Modules.CutScene.Installer;
using CJ.FindAPair.Modules.Meta.Installer;
using CJ.FindAPair.Modules.Service.Installer;
using CJ.FindAPair.Modules.UI.Installer;
using Zenject;

namespace CJ.FindAPair
{
    public class StartupInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            ServiceInstaller.Install(Container);
            UIInstaller.Install(Container);
            CoreGameInstaller.Install(Container);
            MetaGameInstaller.Install(Container);
            CutScenesInstaller.Install(Container);
        }
    }
}