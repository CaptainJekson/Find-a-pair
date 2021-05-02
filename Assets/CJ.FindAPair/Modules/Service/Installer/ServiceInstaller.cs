using CJ.FindAPair.Modules.Service.Save;
using Zenject;

namespace CJ.FindAPair.Modules.Service.Installer
{
    public class ServiceInstaller : Installer<ServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<GameSaver>().AsSingle();
        }
    }
}