using CJ.FindAPair.Modules.CoreGames.Booster;
using CJ.FindAPair.Modules.CoreGames.Configs;
using CJ.FindAPair.Modules.CoreGames.SpecialCards;
using Zenject;

namespace CJ.FindAPair.Modules.CoreGames.Installer
{
    public class CoreGameInstaller : Installer<CoreGameInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<GameSettingsConfig>().FromScriptableObjectResource("Configs/GameSettings/Settings")
                .AsSingle();
            Container.Bind<PlaceCardsConfig>().FromScriptableObjectResource("Configs/GameSettings/PlaceCardsConfig")
                .AsSingle();
            Container.Bind<LevelConfigCollection>().FromScriptableObjectResource("Configs/Collections/LevelCollection")
                .AsSingle();
            
            Container.Bind<LevelCreator>().FromComponentInNewPrefabResource("CoreGames/LevelCreator").AsSingle();
            Container.Bind<CardsPlacer>().AsSingle();
            Container.Bind<LevelBackground>().FromComponentInNewPrefabResource("CoreGames/LevelBackground").AsSingle();
            Container.Bind<GameWatcher>().FromComponentInNewPrefabResource("CoreGames/GameWatcher").AsSingle();
            Container.Bind<RayCaster>().FromComponentInNewPrefabResource("CoreGames/RayCaster").AsSingle();
            Container.Bind<CardComparator>().AsSingle();
            Container.Bind<BoosterHandler>().FromComponentInNewPrefabResource("CoreGames/BoosterHandler").AsSingle();
            Container.Bind<SpecialCardHandler>().FromComponentInNewPrefabResource("CoreGames/SpecialCardHandler")
                .AsSingle();
        }
    }
}
