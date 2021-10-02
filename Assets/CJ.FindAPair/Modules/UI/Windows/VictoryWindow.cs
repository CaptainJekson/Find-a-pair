using System.Linq;
using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.CoreGames.Configs;
using CJ.FindAPair.Modules.UI.Installer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CJ.FindAPair.Modules.UI.Windows
{
    public class VictoryWindow : Window
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private TextMeshProUGUI _currentLevelText;

        private UIRoot _uiRoot;
        private LevelCreator _levelCreator;
        private GameWatcher _gameWatcher;
        private LevelConfigCollection _levelConfigCollection;
    
        [Inject]
        public void Construct(UIRoot uiRoot, LevelCreator levelCreator, GameWatcher gameWatcher, 
            LevelConfigCollection levelConfigCollection)
        {
            _uiRoot = uiRoot;
            _levelCreator = levelCreator;
            _gameWatcher = gameWatcher;
            _levelConfigCollection = levelConfigCollection;
        }

        protected override void Init()
        {
            _gameWatcher.ThereWasAVictory += Open;
            _restartButton.onClick.AddListener(OnRestartButtonClick);
            _exitButton.onClick.AddListener(OnExitButtonClick);
            _nextLevelButton.onClick.AddListener(OnNextLevelButtonClick);
        }
    
        protected override void OnOpen()
        {
            _uiRoot.OpenWindow<GameBlockWindow>();
            _currentLevelText.SetText(_levelCreator.LevelConfig.LevelNumber.ToString());
        }

        protected override void OnClose()
        {
            _uiRoot.CloseWindow<GameBlockWindow>();
        }


        private void OnRestartButtonClick()
        {
            _levelCreator.RestartLevel();
            Close();
        }

        private void OnExitButtonClick()
        {
            _levelCreator.ClearLevel();
            Close();
        }

        private void OnNextLevelButtonClick()
        {
            LevelConfig nextLevel;
            var currentLevelNumber = _levelCreator.LevelConfig.LevelNumber;

            ++currentLevelNumber;
        
            try
            {
                nextLevel = _levelConfigCollection.Levels.First(item => item.LevelNumber == 
                                                                        currentLevelNumber);
            }
            catch
            {
                nextLevel = _levelConfigCollection.Levels.First(item => item.LevelNumber == 1);
            }

            _levelCreator.ClearLevel();
            _levelCreator.CreateLevel(nextLevel);
        
            Close();
        }
    }
}
