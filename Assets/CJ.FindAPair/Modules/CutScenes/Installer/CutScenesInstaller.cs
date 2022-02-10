using CJ.FindAPair.Modules.CutScenes.CutScenes;
using CJ.FindAPair.Modules.CutScenes.CutScenes.Tutorial;
using Zenject;

namespace CJ.FindAPair.Modules.CutScenes.Installer
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
            Container.Bind<GiftBoxCutScene>().AsSingle();
            Container.Bind<TutorialDriver>().AsSingle();
        }
    }
}