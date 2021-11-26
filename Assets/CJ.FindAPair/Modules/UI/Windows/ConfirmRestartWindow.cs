using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.UI.Installer;
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
        private UIRoot _uiRoot;
        private ISaver _gameSaver;

        [Inject]
        public void Construct(LevelCreator levelCreator, EnergyCooldownHandler energyCooldownHandler, UIRoot uiRoot, ISaver gameSaver)
        {
            _levelCreator = levelCreator;
            _energyCooldownHandler = energyCooldownHandler;
            _uiRoot = uiRoot;
            _gameSaver = gameSaver;
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
            _energyCooldownHandler.TryDecreaseScore();
            _uiRoot.OpenWindow<GameInterfaceWindow>();
            Close();
        }
    }
}