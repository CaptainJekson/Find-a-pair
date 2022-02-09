using CJ.FindAPair.Modules.UI.Installer;
using CJ.FindAPair.Modules.UI.Windows;
using Zenject;

public class EnergyBoostOfferWindow : Window
{
    private BlockWindow _blockWindow;
    private AudioController _audioController;
    
    [Inject]
    private void Construct(UIRoot uiRoot, AudioController audioController)
    {
        _blockWindow = uiRoot.GetWindow<BlockWindow>();
        _audioController = audioController;
    }
    
    protected override void OnOpen()
    {
        _audioController.PlaySound(_audioController.AudioClipsCollection.WindowOpenSound);
        _blockWindow.SetOpenWindow(this);
    }
    
    protected override void OnCloseButtonClick()
    {
        _blockWindow.Close();
        base.OnCloseButtonClick();
    }
}