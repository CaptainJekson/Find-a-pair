using System.Linq;
using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.CoreGames.Configs;
using CJ.FindAPair.Modules.UI.Installer;
using DG.Tweening;
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
        [SerializeField] private TextMeshProUGUI _coinsValueText;

        [SerializeField] private float _delayStartCutScene;
        [SerializeField] private float _coinsCountInterval;

        private UIRoot _uiRoot;
        private LevelCreator _levelCreator;
        private GameWatcher _gameWatcher;
        private LevelConfigCollection _levelConfigCollection;
        private ISaver _gameSaver;
        private Sequence _rewardCutSceneSequence;

        [Inject]
        public void Construct(UIRoot uiRoot, LevelCreator levelCreator, GameWatcher gameWatcher, 
            LevelConfigCollection levelConfigCollection, ISaver gameSaver)
        {
            _uiRoot = uiRoot;
            _levelCreator = levelCreator;
            _gameWatcher = gameWatcher;
            _levelConfigCollection = levelConfigCollection;
            _gameSaver = gameSaver;
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
            _levelCreator.OnLevelDeleted += StopRewardCutScene;
            _uiRoot.OpenWindow<GameBlockWindow>();
            _currentLevelText.SetText(_levelCreator.LevelConfig.LevelNumber.ToString());
            PlayRewardCutScene();
        }

        protected override void OnClose()
        {
            _levelCreator.OnLevelDeleted -= StopRewardCutScene;
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

        private void PlayRewardCutScene()
        {
            _rewardCutSceneSequence = DOTween.Sequence();

            int scores = _gameWatcher.Score;
            int coins = _gameSaver.LoadData().ItemsData.Coins - scores;

            _coinsValueText.SetText(coins.ToString());

            _rewardCutSceneSequence.AppendInterval(_delayStartCutScene);
            
            for (int i = 0; i < scores; i++)
            {
                int coinsValue = ++coins;
                
                _rewardCutSceneSequence
                    .AppendCallback(_gameWatcher.DecreaseScore)
                    .AppendCallback(() => _coinsValueText.SetText(coinsValue.ToString()))
                    .AppendInterval(_coinsCountInterval);
            }
        }

        private void StopRewardCutScene()
        {
            _rewardCutSceneSequence.Kill();
            _gameWatcher.ResetScore();
        }
    }
}