using CJ.FindAPair.Modules.CutScene.CutScenes;
using Zenject;

namespace CJ.FindAPair.Modules.CutScene.Installer
{
    public class CutScenesInstaller : Installer<CutScenesInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<CutScenesConfigs>()
                .FromScriptableObjectResource("Configs/Collections/CutScenesConfigs").AsSingle();
            Container.Bind<ScoreObtainCutScene>().AsSingle();
            Container.Bind<ComboValueCutScene>().AsSingle();
            Container.Bind<LevelRewardCutScene>().AsSingle();
            Container.Bind<NextLevelCutScene>().AsSingle();
        }
    }
}