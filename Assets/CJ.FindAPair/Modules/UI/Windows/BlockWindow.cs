using UnityEngine.UI;

namespace CJ.FindAPair.Modules.UI.Windows
{
    public class BlockWindow : Window
    {
        private Window _openWindow;
        
        public void SetOpenWindow(Window window)
        {
            _openWindow = window;
        }
        
        public void CloseButtonInteractable(bool condition)
        {
            GetComponent<Button>().interactable = condition;
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