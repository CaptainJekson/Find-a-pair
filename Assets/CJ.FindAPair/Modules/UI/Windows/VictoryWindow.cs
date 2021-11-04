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
        
        [SerializeField] private Image _coinImage;
        [SerializeField] private Transform _earnedCoinsContainer;
        [SerializeField] private Transform _coinPathPointersContainer;

        [SerializeField] private Vector3 _coinScaledSize;
        [SerializeField] private float _coinScaledTime;
        [SerializeField] private float _delayStartAnimation;
        [SerializeField] private float _intervalBetweenCoins;
        [SerializeField] private float _coinMoveSpeed;

        private List<Image> _rewardCoins;
        private Vector3[] _coinPathPointers;
        
        private UIRoot _uiRoot;
        private LevelCreator _levelCreator;
        private GameWatcher _gameWatcher;
        private LevelConfigCollection _levelConfigCollection;
        private ISaver _gameSaver;
        private Sequence _rewardAnimationSequence;

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
            _rewardCoins = new List<Image>();
            _coinPathPointers = new Vector3[_coinPathPointersContainer.childCount];
            _rewardAnimationSequence = DOTween.Sequence();

            int scores = _gameWatcher.Score;
            int coins = _gameSaver.LoadData().ItemsData.Coins - scores;

            _coinsValueText.SetText(coins.ToString());

            for (int i = 0; i < _coinPathPointersContainer.childCount; i++)
                _coinPathPointers[i] = _coinPathPointersContainer.GetChild(i).transform.position;
            
            for (int i = 0; i < scores; i++)
                _rewardCoins.Add(Instantiate(_coinImage, _earnedCoinsContainer));

            _rewardAnimationSequence.AppendInterval(_delayStartAnimation);

            for (int i = 0; i < scores; i++)
            {
                _rewardAnimationSequence
                    .Append(_rewardCoins[i].transform.DOMove(_coinPathPointersContainer.position, 0))
                    .AppendInterval(_coinScaledTime);
                
                _rewardAnimationSequence
                    .AppendCallback(_gameWatcher.DecreaseScore)
                    .Append(_rewardCoins[i].transform.DOScale(_coinScaledSize, 0))
                    .AppendInterval(_coinScaledTime)
                    .Append(_rewardCoins[i].transform.DOScale(new Vector3(1, 1, 1), 0));
            }

            for (int i = 0; i < scores; i++)
            {
                int currentCoinIndex = i;
                int coinsValue = coins++;

                _rewardAnimationSequence
                    .AppendCallback(() => AnimateCoinReceiving(currentCoinIndex, coinsValue))
                    .AppendInterval(_intervalBetweenCoins);
            }
        }

        private void AnimateCoinReceiving(int currentCoinIndex, int coinsCount)
        {
            Sequence receivingSequence = DOTween.Sequence();

            int gottenCoinsCount = coinsCount + 1;

            receivingSequence
                .Append(_rewardCoins[currentCoinIndex].transform.DOPath(_coinPathPointers, _coinMoveSpeed, PathType.CatmullRom))
                .AppendCallback(() => Destroy(_rewardCoins[currentCoinIndex].gameObject))
                .AppendCallback(() => _coinsValueText.SetText(gottenCoinsCount.ToString()));
            
            receivingSequence
                .Append(_coinImage.transform.DOScale(_coinScaledSize, 0))
                .AppendInterval(_coinScaledTime)
                .Append(_coinImage.transform.DOScale(new Vector3(1, 1, 1), 0));
        }

        private void StopRewardAnimation()
        {
            DOTween.KillAll();
            _gameWatcher.ResetScore();

            for (int i = 0; i < _earnedCoinsContainer.childCount; i++)
                Destroy(_earnedCoinsContainer.GetChild(i).gameObject);
        }
    }
}
