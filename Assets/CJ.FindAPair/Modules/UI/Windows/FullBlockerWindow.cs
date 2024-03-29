using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.UI.Windows.Base;
using Zenject;

namespace CJ.FindAPair.Modules.UI.Windows
{
    public class FullBlockerWindow : Window
    {
        private LevelCreator _levelCreator;
        
        [Inject]
        public void Construct(LevelCreator levelCreator)
        {
            _levelCreator = levelCreator;
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
            foreach (var card in _levelCreator.Cards)
            {
                card.EnableInteractable();
            }
        }

        protected override void PlayOpenSound()
        {
        }

        protected override void PlayCloseSound()
        {
        }
    }
}