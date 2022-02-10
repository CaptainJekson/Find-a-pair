using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.UI.Windows.Base;
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