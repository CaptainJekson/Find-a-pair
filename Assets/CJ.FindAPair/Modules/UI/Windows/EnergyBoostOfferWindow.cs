using CJ.FindAPair.Modules.UI.Installer;
using CJ.FindAPair.Modules.UI.Windows.Base;
using Zenject;

namespace CJ.FindAPair.Modules.UI.Windows
{
    public class EnergyBoostOfferWindow : Window
    {
        private BlockWindow _blockWindow;

        [Inject]
        private void Construct(UIRoot uiRoot)
        {
            _blockWindow = uiRoot.GetWindow<BlockWindow>();
        }
    
        protected override void OnOpen()
        {
            base.OnOpen();
            _blockWindow.SetOpenWindow(this);
        }
    
        protected override void OnCloseButtonClick()
        {
            _blockWindow.Close();
            base.OnCloseButtonClick();
        }

        protected override void PlayCloseSound()
        {
        }
    }
}