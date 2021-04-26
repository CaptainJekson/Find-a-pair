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

        [Inject]
        public void Construct(LevelCreator levelCreator)
        {
            _levelCreator = levelCreator;
        }

        protected override void Init()
        {
            _restartButton.onClick.AddListener(OnRestartButtonClick);
        }

        private void OnRestartButtonClick()
        {
            _levelCreator.RestartLevel();
        }
    }
}
