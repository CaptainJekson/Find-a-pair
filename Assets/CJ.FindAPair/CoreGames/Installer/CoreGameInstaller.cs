using System.ComponentModel;
using CJ.FindAPair.CardTable;
using CJ.FindAPair.Configuration;
using CJ.FindAPair.Configurations;
using CJ.FindAPair.CoreGames.SpecialCards;
using CJ.FindAPair.CoreGames.TEST;
using Zenject;

namespace CJ.FindAPair.CoreGames.Installer
{
    public class CoreGameInstaller : Installer<CoreGameInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<GameSettingsConfig>().FromScriptableObjectResource("Configs/GameSettings/Settings")
                .AsSingle();
            Container.Bind<LevelConfigCollection>().FromScriptableObjectResource("Configs/Collections/LevelCollection")
                .AsSingle();
            
            Container.Bind<LevelCreator>().FromComponentInNewPrefabResource("CoreGames/CardTable").AsSingle();
            Container.Bind<GameWatcher>().FromComponentInNewPrefabResource("CoreGames/GameWatcher").AsSingle();
            Container.Bind<CardComparator>().FromComponentInNewPrefabResource("CoreGames/CardComparator").AsSingle();
            Container.Bind<BoosterHandler>().FromComponentInNewPrefabResource("CoreGames/BoosterHandler").AsSingle();
            Container.Bind<SpecialCardHandler>().FromComponentInNewPrefabResource("CoreGames/SpecialCardHandler")
                .AsSingle();
            Container.Bind<CreateLevelTEST>().AsSingle(); // TODO Test
        }
    }
}
