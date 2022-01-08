using Zenject;

namespace CJ.FindAPair.Modules.UI.Installer
{
    public class UIInstaller : Installer<UIInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<UIRoot>().FromComponentInNewPrefabResource("UI/UIRoot").AsSingle();
            Container.Bind<TutorialRoot>().FromComponentInNewPrefabResource("UI/TutorialRoot").AsSingle();
            Container.Bind<LevelMarker>().FromComponentInNewPrefabResource("UI/LevelMarker").AsSingle();
        }
    }
}