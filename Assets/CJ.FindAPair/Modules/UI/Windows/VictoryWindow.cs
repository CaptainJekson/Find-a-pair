using System.Collections.Generic;
using System.Linq;
using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.CoreGames.Configs;
using CJ.FindAPair.Modules.UI.Installer;
using DG.Tweening;
using Doozy.Engine.Extensions;
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

        [SerializeField] private GameInterfaceWindow _gameInterfaceWindow;
        [SerializeField] private GameObject _item;
        [SerializeField] private Transform _clonesContainer;
        [SerializeField] private float _delayStartAnimation;
        [SerializeField] private float _intervalBetweenCoins;
        [SerializeField] private float _coinSpeed;
        [SerializeField] private Ease _transferEase;

        private UIRoot _uiRoot;
        private LevelCreator _levelCreator;
        private GameWatcher _gameWatcher;
        private LevelConfigCollection _levelConfigCollection;
        private ISaver _gameSaver;
        private Transferer _transferer;
        private Sequence _rewardAnimationSequence;
        private List<GameObject> _itemClones;

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
            _levelCreator.OnLevelDeleted += StopRewardAnimation;
            _uiRoot.OpenWindow<GameBlockWindow>();
            _currentLevelText.SetText(_levelCreator.LevelConfig.LevelNumber.ToString());
            PlayRewardAnimation();
        }

        protected override void OnClose()
        {
            _levelCreator.OnLevelDeleted -= StopRewardAnimation;
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

        private void PlayRewardAnimation()
        {
            _rewardAnimationSequence = DOTween.Sequence();
            _itemClones = new List<GameObject>();

            int scores = _gameWatcher.Score;
            int coins = _gameSaver.LoadData().ItemsData.Coins - scores;

            for (int i = 0; i < scores; i++)
                _itemClones.Add(Instantiate(_item, _clonesContainer));
            
            _coinsValueText.SetText(coins.ToString());

            _rewardAnimationSequence.AppendInterval(_delayStartAnimation);

            for (int i = 0; i < scores; i++)
            {
                int coinsValue = coins++;
                int itemIndex = i;

                _rewardAnimationSequence
                    .AppendCallback(_gameWatcher.DecreaseScore)
                    .AppendCallback(() => AnimateReceiving(itemIndex, coinsValue))
                    .AppendInterval(_intervalBetweenCoins);
            }
        }

        private void AnimateReceiving(int itemIndex, int coinsValue)
        {
            int gottenCoinsCount = coinsValue + 1;
                
                _transferer.TransferItem(_itemClones[itemIndex], _gameInterfaceWindow.GetCoinPosition(), _item.transform.position, _coinSpeed, _transferEase)
                    .AppendCallback(() => _coinsValueText.SetText(gottenCoinsCount.ToString()));
        }

        private void StopRewardAnimation()
        {
            _gameWatcher.ResetScore();
            _rewardAnimationSequence.Kill();
        }
    }
}
