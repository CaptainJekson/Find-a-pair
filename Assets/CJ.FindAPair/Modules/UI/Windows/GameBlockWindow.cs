using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.UI.Installer;
using Zenject;

namespace CJ.FindAPair.Modules.UI.Windows
{
    public class GameBlockWindow : Window
    {
        private LevelCreator _levelCreator;
        private UIRoot _uiRoot;
        
        [Inject]
        public void Construct(LevelCreator levelCreator, UIRoot uiRoot)
        {
            _levelCreator = levelCreator;
            _uiRoot = uiRoot;
        }
        
        protected override void OnOpen()
        {
            foreach (var card in _levelCreator.Cards)
            {
                card.DisableInteractable();
            }
        }

        protected override void OnClose()
        {
            if (_uiRoot.GetWindow<FullBlockerWindow>().gameObject.activeSelf == false)
            {
                foreach (var card in _levelCreator.Cards)
                {
                    card.EnableInteractable();
                }
            }
        }
    }
}