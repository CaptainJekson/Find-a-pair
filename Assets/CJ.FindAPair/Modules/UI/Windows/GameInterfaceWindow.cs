using System;
using System.Collections.Generic;
using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.UI.Installer;
using CJ.FindAPair.Utility;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CJ.FindAPair.Modules.UI.Windows
{
    public class GameInterfaceWindow : Window
    {
        [SerializeField] private TextMeshProUGUI _lifeValueText;
        [SerializeField] private TextMeshProUGUI _timeValueText;
        [SerializeField] private TextMeshProUGUI _scoreValueText;
        [SerializeField] private TextMeshProUGUI _configAdsText;
        [SerializeField] private Image _lockImage;
        [SerializeField] private Transform _gottenCoinsTransform;
        [SerializeField] private ItemToTransfer _itemToTransfer;

        private GameWatcher _gameWatcher;
        private LevelCreator _levelCreator;
        private EnergyCooldownHandler _energyCooldownHandler;
        private UIRoot _uiRoot;
        private CardComparator _cardComparator;
        private Transferer _transferer;
        
        public Vector3 GottenCoinsPosition => _gottenCoinsTransform.transform.position;

        [Inject]
        public void Construct(GameWatcher gameWatcher, LevelCreator levelCreator, 
            EnergyCooldownHandler energyCooldownHandler, UIRoot uiRoot, CardComparator cardComparator, Transferer transferer)
        {
            _levelCreator = levelCreator;
            _gameWatcher = gameWatcher;
            _energyCooldownHandler = energyCooldownHandler;
            _uiRoot = uiRoot;
            _cardComparator = cardComparator;
            _transferer = transferer;
        }

        protected override void OnOpen()
        {
            _cardComparator.CardsMatched += GetCoins;
            _gameWatcher.LifeСhanged += SetLifeValue;
            _gameWatcher.ScoreСhanged += SetScoreValue;
            _gameWatcher.TimeСhanged += SetTimeValue;
            _gameWatcher.ConfirmShowAds += ShowConfigAdsText;
            _gameWatcher.ThereWasAVictory += HideConfigAdsText;
            _gameWatcher.ThereWasADefeat += HideConfigAdsText;
            SetData();
            HideConfigAdsText();
            
            SetIncomeLockImage();
        }

        protected override void OnClose()
        {
            _cardComparator.CardsMatched -= GetCoins;
            _gameWatcher.LifeСhanged -= SetLifeValue;
            _gameWatcher.ScoreСhanged -= SetScoreValue;
            _gameWatcher.TimeСhanged -= SetTimeValue;
            _gameWatcher.ConfirmShowAds -= ShowConfigAdsText;
            _gameWatcher.ThereWasAVictory -= HideConfigAdsText;
            _gameWatcher.ThereWasADefeat -= HideConfigAdsText;
        }

        private void OnApplicationQuit()
        {
            if (_uiRoot.GetWindow<VictoryWindow>().gameObject.activeSelf == false)
            {
                _energyCooldownHandler.DecreaseScore();
            }
        }

        public void SetIncomeLockImage()
        {
            _lockImage.gameObject.SetActive(!_gameWatcher.IsIncomeLevel());
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

        public void DecreaseScores(int scoreValue, int endScoreValue, float decreaseDuration)
        {
            _scoreValueText.ChangeOfNumericValueForText(scoreValue, endScoreValue, decreaseDuration);
        }

        private void GetCoins()
        {
            Sequence sequence = DOTween.Sequence();
            List<Transform> vectors = new List<Transform>();

            for (int i = 0; i < _cardComparator.ComparisonCards.Count; i++)
            {
                vectors.Add(_cardComparator.ComparisonCards[i].transform);
            }

            for (int i = 0; i < _cardComparator.ComparisonCards.Count; i++)
            {
                int interaction = i;
                var item = Instantiate(_itemToTransfer, transform);

                sequence
                    .AppendCallback(() => _transferer.TransferItem(item.transform,
                        vectors[interaction].position, _gottenCoinsTransform.position, 5f));
            }
        }
    }
}