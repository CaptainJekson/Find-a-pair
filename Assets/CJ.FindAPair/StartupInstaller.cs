using CJ.FindAPair.CoreGames.Installer;
using CJ.FindAPair.Service.Installer;
using CJ.FindAPair.UI.Installer;
using Zenject;

public class StartupInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        CoreGameInstaller.Install(Container);
        ServiceInstaller.Install(Container);
        UIInstaller.Install(Container);
    }
}
