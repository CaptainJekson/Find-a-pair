using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.UI.Windows.Base;
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

        [Inject]
        public void Construct(LevelCreator levelCreator, EnergyCooldownHandler energyCooldownHandler, 
            GameWatcher gameWatcher)
        {
            _levelCreator = levelCreator;
            _energyCooldownHandler = energyCooldownHandler;
            _gameWatcher = gameWatcher;
        }

        protected override void OnOpen()
        {
            base.OnOpen();
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