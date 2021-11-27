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

        [Inject]
        public void Construct(LevelCreator levelCreator, EnergyCooldownHandler energyCooldownHandler)
        {
            _levelCreator = levelCreator;
            _energyCooldownHandler = energyCooldownHandler;
        }

        protected override void OnOpen()
        {
            _restartButton.onClick.AddListener(OnRestartButtonClick);
        }

        protected override void OnClose()
        {
            _restartButton.onClick.RemoveListener(OnRestartButtonClick);
        }

        private void OnRestartButtonClick()
        {
            _levelCreator.RestartLevel();
            _energyCooldownHandler.DecreaseScore();
        }
    }
}