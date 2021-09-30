using CJ.FindAPair.Modules.Service.Ads;
using CJ.FindAPair.Modules.Service.Ads.Configs;
using CJ.FindAPair.Modules.Service.Save;
using Zenject;

namespace CJ.FindAPair.Modules.Service.Installer
{
    public class ServiceInstaller : Installer<ServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<UnityAdsConfig>().FromScriptableObjectResource("Configs/Services/UnityAdsConfig")
                .AsSingle();
            
            Container.Bind<ISaver>().To<JsonSaver>().AsSingle();
            Container.Bind<IAdsDriver>().To<UnityAdsDriver>().AsSingle();
        }
    }
}