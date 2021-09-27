using System;
using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.CoreGames.SpecialCards;
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
        [SerializeField] private TextMeshProUGUI _currentLevelText;
        [SerializeField] private TextMeshProUGUI _defeatNotificationText;
        [SerializeField] private string _timeIsOverMessage;
        [SerializeField] private string _livesAreOverMessage;
        [SerializeField] private string _bombDetonatedMessage;
        [SerializeField] private string _fortuneCardRealisedMessage;

        private LevelCreator _levelCreator;
        private GameWatcher _gameWatcher;
        private BombCard _bombCard;
        private FortuneCard _fortuneCard;

        [Inject]
        public void Construct(LevelCreator levelCreator, GameWatcher gameWatcher,
            SpecialCardHandler specialCardHandler)
        {
            _levelCreator = levelCreator;
            _gameWatcher = gameWatcher;
            _bombCard = specialCardHandler.GetComponentInChildren<BombCard>();
            _fortuneCard = specialCardHandler.GetComponentInChildren<FortuneCard>();
        }

        protected override void Init()
        {
            _gameWatcher.TimeIsOut += TimeIsOver;
            _gameWatcher.LivesIsOut += LivesAreOver;
            _bombCard.BombDetonate += BombDetonated;
            _fortuneCard.CardRealised += FortuneCardRealised;
            _gameWatcher.ThereWasADefeat += Open;

            _restartButton.onClick.AddListener(OnRestartButtonClick);
            _exitButton.onClick.AddListener(OnExitButtonClick);
            _adsButton.onClick.AddListener(OnAdsButtonClick);
        }

        protected override void OnOpen()
        {
            _currentLevelText.SetText(_levelCreator.LevelConfig.LevelNumber.ToString());
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

        private void OnAdsButtonClick()
        {
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