using CJ.FindAPair.Modules.CoreGames;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CJ.FindAPair.Modules.UI.Windows
{
    public class ConfirmRestartWindow : Window
    {
        [SerializeField] private Button _restartButton;

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
            _restartButton.onClick.AddListener(OnRestartButtonClick);
        }

        protected override void OnClose()
        {
            _restartButton.onClick.RemoveListener(OnRestartButtonClick);
        }

        private void OnRestartButtonClick()
        {
            _levelCreator.RestartLevel();

            if (_gameWatcher.IsIncomeLevel())
                _energyCooldownHandler.DecreaseScore();
        }
    }
}