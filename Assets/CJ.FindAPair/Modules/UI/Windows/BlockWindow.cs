namespace CJ.FindAPair.Modules.UI.Windows
{
    public class BlockWindow : Window
    {
        private Window _openWindow;
        
        public void SetOpenWindow(Window window)
        {
            _openWindow = window;
        }
        
        public void SetCloseButtonCondition(bool condition)
        {
            if (_closeButton != null)
                _closeButton.interactable = condition;
        }
        
        protected override void OnCloseButtonClick()
        {
            if (_openWindow != null)
            {
                _openWindow.Close();
                base.OnCloseButtonClick();
            }
        }
    }
}