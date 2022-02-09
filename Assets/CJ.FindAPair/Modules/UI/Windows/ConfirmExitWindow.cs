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
        private GameWatcher _gameWatcher;
        private AudioController _audioController;

        [Inject]
        public void Construct(LevelCreator levelCreator, EnergyCooldownHandler energyCooldownHandler, 
            GameWatcher gameWatcher, AudioController audioController)
        {
            _levelCreator = levelCreator;
            _energyCooldownHandler = energyCooldownHandler;
            _gameWatcher = gameWatcher;
            _audioController = audioController;
        }

        protected override void OnOpen()
        {
            _audioController.PlaySound(_audioController.AudioClipsCollection.WindowOpenSound);
            _exitButton.onClick.AddListener(OnExitButtonClick);
        }

        protected override void OnClose()
        {
            _exitButton.onClick.RemoveListener(OnExitButtonClick);
        }
        
        private void OnExitButtonClick()
        {
            _levelCreator.ClearLevel();
            
            if (_gameWatcher.IsIncomeLevel())
                _energyCooldownHandler.DecreaseScore();
        }
    }
}