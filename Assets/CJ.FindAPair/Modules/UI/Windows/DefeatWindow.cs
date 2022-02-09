using System;
using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.CoreGames.Configs;
using CJ.FindAPair.Modules.CoreGames.SpecialCards;
using CJ.FindAPair.Modules.Service.Ads;
using CJ.FindAPair.Modules.Service.Ads.Configs;
using CJ.FindAPair.Modules.UI.Installer;
using I2.Loc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CJ.FindAPair.Modules.UI.Windows
{
    public class DefeatWindow : Window
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _adsButton;
        [SerializeField] private Image _loadingAdsBlocker;
        [SerializeField] private Image _timerAdsBlocker;
        [SerializeField] private TextMeshProUGUI _timeText;
        [SerializeField] private TextMeshProUGUI _currentLevelText;
        [SerializeField] private TextMeshProUGUI _defeatNotificationText;
        
        [Header("DEFEAT MESSAGES:")]
        [SerializeField] private LocalizedString _timeIsOverMessage;
        [SerializeField] private LocalizedString _livesAreOverMessage;
        [SerializeField] private LocalizedString _bombDetonatedMessage;
        [SerializeField] private LocalizedString _fortuneCardRealisedMessage;

        private UIRoot _uiRoot;
        private LevelCreator _levelCreator;
        private GameSettingsConfig _gameSettingsConfig;
        private GameWatcher _gameWatcher;
        private BombCard _bombCard;
        private FortuneCard _fortuneCard;
        private ISaver _gameSaver;
        private IAdsDriver _adsDriver;
        private UnityAdsConfig _unityAdsConfig;
        private EnergyCooldownHandler _energyCooldownHandler;
        private AudioController _audioController;
        private DateTime _endCooldownForContinueGame = DateTime.Now;

        [Inject]
        public void Construct(UIRoot uiRoot, LevelCreator levelCreator, GameSettingsConfig gameSettingsConfig,
            GameWatcher gameWatcher, SpecialCardHandler specialCardHandler, ISaver gameSaver, IAdsDriver adsDriver, 
            UnityAdsConfig unityAdsConfig, EnergyCooldownHandler energyCooldownHandler, AudioController audioController)
        {
            _uiRoot = uiRoot;
            _levelCreator = levelCreator;
            _gameSettingsConfig = gameSettingsConfig;
            _gameWatcher = gameWatcher;
            _gameSaver = gameSaver;
            _adsDriver = adsDriver;
            _unityAdsConfig = unityAdsConfig;
            _bombCard = specialCardHandler.GetComponentInChildren<BombCard>();
            _fortuneCard = specialCardHandler.GetComponentInChildren<FortuneCard>();
            _energyCooldownHandler = energyCooldownHandler;
            _audioController = audioController;
        }

        private void Update()
        {
            if (_endCooldownForContinueGame > DateTime.Now)
            {
                _timerAdsBlocker.gameObject.SetActive(true);
                ShowTickTime();
            }
            else
            {
                _timerAdsBlocker.gameObject.SetActive(false);
            }
        }
        
        protected override void Init()
        {
            _gameWatcher.TimeIsOut += TimeIsOver;
            _gameWatcher.LivesIsOut += LivesAreOver;
            _bombCard.BombDetonate += BombDetonated;
            _fortuneCard.CardRealised += FortuneCardRealised;
            _gameWatcher.ThereWasADefeat += Open;
            _adsDriver.AdsIsReady += UnLockAdsLoadingBlocker;
            _adsDriver.AdsIsComplete += LockAdsTimerBlocker;

            _restartButton.onClick.AddListener(OnRestartButtonClick);
            _exitButton.onClick.AddListener(OnExitButtonClick);
            _adsButton.onClick.AddListener(OnAdsButtonClick);
        }

        protected override void OnOpen()
        {
            _uiRoot.OpenWindow<GameBlockWindow>();
            _currentLevelText.SetText(_levelCreator.LevelConfig.LevelNumber.ToString());
            _audioController.StopMusic();
            _audioController.PlaySound(_audioController.AudioClipsCollection.DefeatSound);
            
            var gameData = _gameSaver.LoadData();

            if (DateTime.TryParse(gameData.AdsData.EndCooldownForContinueGame, out var parseResult))
            {
                _endCooldownForContinueGame = parseResult;
            }
            
            _energyCooldownHandler.DecreaseScore();
        }

        protected override void OnClose()
        {
            _uiRoot.CloseWindow<GameBlockWindow>();
        }

        private void OnRestartButtonClick()
        {
            if (_gameSaver.LoadData().ItemsData.Energy <= 0)
            {
                _uiRoot.OpenWindow<EnergyBoostOfferWindow>();
            }
            else
            {
                _levelCreator.RestartLevel();
                Close();
            }
        }

        private void OnExitButtonClick()
        {
            _levelCreator.ClearLevel();
            Close();
        }

        private void OnAdsButtonClick()
        {
            _gameWatcher.ContinueGameWithAdsInEnd();
            _loadingAdsBlocker.gameObject.SetActive(true);
            Close();
        }

        private void UnLockAdsLoadingBlocker(string placementId)
        {
            if (placementId == _unityAdsConfig.PlacementRewardedVideoId)
            {
                _loadingAdsBlocker.gameObject.SetActive(false);
            }
        }
        
        private void LockAdsTimerBlocker(string placementId)
        {
            if (placementId == _unityAdsConfig.PlacementRewardedVideoId)
            {
                var gameData = _gameSaver.LoadData();
                var dateTimeNow = DateTime.Now;
                _endCooldownForContinueGame = dateTimeNow
                    .AddSeconds(_gameSettingsConfig.CooldownAdsContinueGameInSecond);
                gameData.AdsData.EndCooldownForContinueGame = _endCooldownForContinueGame.ToString();
                _gameSaver.SaveData(gameData);
            }
        }
        
        private void ShowTickTime()
        {
            var timeInterval = _endCooldownForContinueGame - DateTime.Now;
            _timeText.text = timeInterval.ToString(@"mm\:ss");
        }
        
        private void TimeIsOver()
        {
            _defeatNotificationText.SetText(_timeIsOverMessage);
        }

        private void LivesAreOver()
        {
            _defeatNotificationText.SetText(_livesAreOverMessage);
        }

        private void BombDetonated()
        {
            _defeatNotificationText.SetText(_bombDetonatedMessage);
        }

        private void FortuneCardRealised()
        {
            _defeatNotificationText.SetText(_fortuneCardRealisedMessage);
        }
    }
}