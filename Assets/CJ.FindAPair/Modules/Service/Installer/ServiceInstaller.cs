using CJ.FindAPair.Modules.Service.Ads;
using CJ.FindAPair.Modules.Service.Ads.Configs;
using CJ.FindAPair.Modules.Service.Audio;
using CJ.FindAPair.Modules.Service.Save;
using CJ.FindAPair.Modules.Service.Server;
using CJ.FindAPair.Modules.Service.Server.Configs;
using CJ.FindAPair.Modules.Service.Store;
using Zenject;

namespace CJ.FindAPair.Modules.Service.Installer
{
    public class ServiceInstaller : Installer<ServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<UnityAdsConfig>().FromScriptableObjectResource("Configs/Services/UnityAdsConfig")
                .AsSingle();
            
            Container.Bind<ServerConfig>().FromScriptableObjectResource("Configs/Services/ServerConfig")
                .AsSingle();
     
            Container.Bind<ServerConnector>().FromComponentInNewPrefabResource("Services/ServerConnector").AsSingle();
            Container.Bind<ISaver>().To<JsonSaver>().AsSingle();
            Container.Bind<IAdsDriver>().To<UnityAdsDriver>().AsSingle();
            Container.Bind<IStoreDriver>().To<StoreDriver>().AsSingle();
            Container.Bind<AudioController>().FromComponentInNewPrefabResource("Services/AudioController").AsSingle();
        }
    }
}