using CJ.FindAPair.Modules.Meta.Configs;
using CJ.FindAPair.Modules.Meta.Themes;
using Zenject;

namespace CJ.FindAPair.Modules.Meta.Installer
{
    public class MetaGameInstaller : Installer<MetaGameInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<ThemeConfigCollection>().FromScriptableObjectResource("Configs/Collections/ThemeCollection")
                .AsSingle();
            
            Container.Bind<SpecialCardImageConfig>().FromScriptableObjectResource("Configs/Themes/SpecialCardImageConfig")
                .AsSingle();
            
            Container.Bind<ThemesSelector>().AsSingle();
        }
    }
}