using CJ.FindAPair.Configuration;
using CJ.FindAPair.Configurations;
using Zenject;

namespace CJ.FindAPair.CoreGames.Installer
{
    public class CoreGameInstaller : Installer<CoreGameInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<GameSettingsConfig>().FromScriptableObjectResource("Configs/GameSettings/Settings").AsSingle();
            Container.Bind<LevelConfigCollection>().FromScriptableObjectResource("Configs/Collections/LevelCollection")
                .AsSingle();

            Container.Bind<GameCamera>().FromComponentInNewPrefabResource("CoreGames/MainCamera").AsSingle();
            Container.Bind<LevelCreator>().FromComponentInNewPrefabResource("CoreGames/CardTableCanvas").AsSingle();
            Container.Bind<GameWatcher>().FromComponentInNewPrefabResource("CoreGames/GameWatcher").AsSingle();
            Container.Bind<CardComparator>().FromComponentInNewPrefabResource("CoreGames/CardComparator").AsSingle();
        }
    }
}
