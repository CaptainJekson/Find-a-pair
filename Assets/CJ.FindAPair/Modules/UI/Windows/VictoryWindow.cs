using System.Collections.Generic;
using System.Linq;
using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.CoreGames.Configs;
using CJ.FindAPair.Modules.UI.Installer;
using CJ.FindAPair.Utility;
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

        [SerializeField] private ItemToTransfer _itemToTransfer;
        [SerializeField] private Transform _coinsParentTransform;
        [SerializeField] private float _coinMoveDuration;
        [SerializeField] private float _distanceBetweenCoins;
        [SerializeField] private float _delayStartCutScene;
        [SerializeField] private Ease _transferCoinEase;

        private UIRoot _uiRoot;
        private LevelCreator _levelCreator;
        private GameWatcher _gameWatcher;
        private LevelConfigCollection _levelConfigCollection;
        private ISaver _gameSaver;
        private Transferer _transferer;
        private GameInterfaceWindow _gameInterfaceWindow;
        private Sequence _rewardCutSceneSequence;
        private List<ItemToTransfer> _spawnedCoins;

        [Inject]
        public void Construct(UIRoot uiRoot, LevelCreator levelCreator, GameWatcher gameWatcher, 
            LevelConfigCollection levelConfigCollection, ISaver gameSaver, Transferer transferer)
        {
            _uiRoot = uiRoot;
            _levelCreator = levelCreator;
            _gameWatcher = gameWatcher;
            _levelConfigCollection = levelConfigCollection;
            _gameSaver = gameSaver;
            _transferer = transferer;
            _gameInterfaceWindow = _uiRoot.GetWindow<GameInterfaceWindow>();
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
            _spawnedCoins = new List<ItemToTransfer>();
            
            int rewardCoins = _gameWatcher.Score;
            int currentCoins = _gameSaver.LoadData().ItemsData.Coins - rewardCoins;

            for (int i = 0; i < rewardCoins; i++)
                _spawnedCoins.Add(Instantiate(_itemToTransfer, _coinsParentTransform));

            _coinsValueText.SetText(currentCoins.ToString());

            _rewardCutSceneSequence
                .AppendInterval(_delayStartCutScene);

            for (int i = 0; i < rewardCoins; i++)
            {
                int coinsValue = ++currentCoins;
                int iterationNumber = i;
                int decreasedScoresValue = rewardCoins - i;

                _rewardCutSceneSequence
                    .AppendCallback(() => _gameInterfaceWindow.DecreaseScores(decreasedScoresValue, --decreasedScoresValue, 0))
                    .AppendCallback(() => TransferCoin(iterationNumber, coinsValue))
                    .AppendInterval(_distanceBetweenCoins);
            }
        }

        private void TransferCoin(int spawnedCoinNumber, int coins)
        {
            Sequence transferSequence = DOTween.Sequence();

            transferSequence
                .AppendCallback(() => _transferer.TransferItem(_spawnedCoins[spawnedCoinNumber].transform,
                    _gameInterfaceWindow.GottenCoinsPosition, _itemToTransfer.transform.position, _coinMoveDuration, _transferCoinEase))
                .AppendInterval(_coinMoveDuration)
                .AppendCallback(() => _coinsValueText.SetText(coins.ToString()));
        }
        
        private void StopRewardCutScene()
        {
            _rewardCutSceneSequence.Kill();
            _gameWatcher.ResetScore();

            for (int i = 0; i < _spawnedCoins.Count; i++)
                Destroy(_spawnedCoins[i].gameObject);
        }
    }
}