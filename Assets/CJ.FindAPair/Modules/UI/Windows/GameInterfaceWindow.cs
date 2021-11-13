using System;
using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Utility;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace CJ.FindAPair.Modules.UI.Windows
{
    public class GameInterfaceWindow : Window
    {
        [SerializeField] private TextMeshProUGUI _lifeValueText;
        [SerializeField] private TextMeshProUGUI _timeValueText;
        [SerializeField] private TextMeshProUGUI _scoreValueText;

        [SerializeField] private TextMeshProUGUI _configAdsText;

        private GameWatcher _gameWatcher;
        private LevelCreator _levelCreator;

        [Inject]
        public void Construct(GameWatcher gameWatcher, LevelCreator levelCreator)
        {
            _levelCreator = levelCreator;
            _gameWatcher = gameWatcher;
        }

        protected override void OnOpen()
        {
            _gameWatcher.LifeСhanged += SetLifeValue;
            _gameWatcher.ScoreСhanged += SetScoreValue;
            _gameWatcher.TimeСhanged += SetTimeValue;
            _gameWatcher.ConfirmShowAds += ShowConfigAdsText;
            _gameWatcher.ThereWasAVictory += HideConfigAdsText;
            _gameWatcher.ThereWasADefeat += HideConfigAdsText;
            SetData();
            HideConfigAdsText();
        }

        protected override void OnClose()
        {
            _gameWatcher.LifeСhanged -= SetLifeValue;
            _gameWatcher.ScoreСhanged -= SetScoreValue;
            _gameWatcher.TimeСhanged -= SetTimeValue;
            _gameWatcher.ConfirmShowAds -= ShowConfigAdsText;
            _gameWatcher.ThereWasAVictory -= HideConfigAdsText;
            _gameWatcher.ThereWasADefeat -= HideConfigAdsText;
        }
    
        private void SetData()
        {
            SetLifeValue(_levelCreator.LevelConfig.Tries);
            SetScoreValue(0);
            SetTimeValue(_levelCreator.LevelConfig.Time);
        }
    
        private void SetLifeValue(int value)
        {
            _lifeValueText.SetText(value.ToString());
        }
    
        private void SetScoreValue(int value)
        {
            _scoreValueText.SetText(value.ToString());
        }
    
        private void SetTimeValue(int value)
        {
            _timeValueText.SetText(TimeConvert(value));
        }

        private string TimeConvert(int secondTime)
        {
            var time = TimeSpan.FromSeconds(secondTime);

            return time.ToString(@"mm\:ss");
        }

        private void ShowConfigAdsText()
        {
            _configAdsText.gameObject.SetActive(true);
        }
        
        private void HideConfigAdsText()
        {
            _configAdsText.gameObject.SetActive(false);
        }

        public void DecreaseScores(float decreaseTime, int scores, Ease decreaseEase)
        {
            _scoreValueText.ChangeOfNumericValueForText(scores, 0, decreaseTime, decreaseEase);
        }
    }
}