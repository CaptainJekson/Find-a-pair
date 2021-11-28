using CJ.FindAPair.Modules.CoreGames;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CJ.FindAPair.Modules.UI.Windows
{
    public class ConfirmExitWindow : Window
    {
        [SerializeField] private Button _exitButton;
        
        private LevelCreator _levelCreator;
        private EnergyCooldownHandler _energyCooldownHandler;

        [Inject]
        public void Construct(LevelCreator levelCreator, EnergyCooldownHandler energyCooldownHandler)
        {
            _levelCreator = levelCreator;
            _energyCooldownHandler = energyCooldownHandler;
        }

        protected override void OnOpen()
        {
            _exitButton.onClick.AddListener(OnExitButtonClick);
        }

        protected override void OnClose()
        {
            _exitButton.onClick.RemoveListener(OnExitButtonClick);
        }
        
        private void OnExitButtonClick()
        {
            _levelCreator.ClearLevel();
            _energyCooldownHandler.DecreaseScore();
        }
    }
}