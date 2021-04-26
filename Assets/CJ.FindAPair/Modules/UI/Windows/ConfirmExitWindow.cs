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

        [Inject]
        public void Construct(LevelCreator levelCreator)
        {
            _levelCreator = levelCreator;
        }

        protected override void Init()
        {
            _exitButton.onClick.AddListener(OnExitButtonClick);
        }
        
        private void OnExitButtonClick()
        {
            _levelCreator.ClearLevel();
        }
    }
}
