using System;
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
        [Header("Game indicators")] 
        [SerializeField] private TextMeshProUGUI _lifeValueText;
        [SerializeField] private TextMeshProUGUI _timeValueText;
        [SerializeField] private TextMeshProUGUI _scoreValueText;
        [SerializeField] private TextMeshProUGUI _configAdsText;
        [SerializeField] private Image _lockImage;

        [Header("Score receiving cut scene settings")] 
        [SerializeField] private Transform _gottenCoinsTransform;
        [SerializeField] private FlyCoin _coinToTransfer;
        [SerializeField] private float _receiveCoinDuration;
        [SerializeField] private Ease _transferScoreEase;
        [SerializeField] private TextMeshProUGUI _comboValueText;
        [SerializeField] private float _comboValueShowDuration;
        [SerializeField] private float _comboValueRecoil;
        [SerializeField] private Ease _comboValueTransferEase;

        private GameWatcher _gameWatcher;
        private LevelCreator _levelCreator;
        private EnergyCooldownHandler _energyCooldownHandler;
        private UIRoot _uiRoot;
        private CardComparator _cardComparator;
        private Transferer _transferer;
        private Camera _camera;

        public Vector3 GottenCoinsPosition => _gottenCoinsTransform.transform.position;

        [Inject]
        public void Construct(GameWatcher gameWatcher, LevelCreator levelCreator,
            EnergyCooldownHandler energyCooldownHandler, UIRoot uiRoot, CardComparator cardComparator,
            Transferer transferer)
        {
            _levelCreator = levelCreator;
            _gameWatcher = gameWatcher;
            _energyCooldownHandler = energyCooldownHandler;
            _uiRoot = uiRoot;
            _cardComparator = cardComparator;
            _transferer = transferer;
            _camera = Camera.main;
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
            SetIncomeLockImage();
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

        public void PlayReceiveScoresCutScene()
        {
            Sequence receiveCoinsSequence = DOTween.Sequence();

            var lastOpenedCard = _cardComparator.ComparisonCards[_cardComparator.ComparisonCards.Count - 1];
            var item = Instantiate(_coinToTransfer, _gottenCoinsTransform);
            var itemStartPosition = _camera.WorldToScreenPoint(lastOpenedCard.transform.position);

            item.gameObject.SetActive(true);

            receiveCoinsSequence
                .AppendCallback(() => _transferer.TransferItem(item.transform,
                    itemStartPosition, _gottenCoinsTransform.position, _receiveCoinDuration, _transferScoreEase));

            if (_gameWatcher.ComboCounter > 1)
            {
                receiveCoinsSequence
                    .AppendCallback(() => ShowComboValue(itemStartPosition, _gameWatcher.ScoreCombo));
            }

            receiveCoinsSequence
                .AppendInterval(_receiveCoinDuration)
                .AppendCallback(() => Destroy(item.gameObject));
        }

        private void ShowComboValue(Vector3 itemStartPosition, int scoreCombo)
        {
            Sequence comboValueShowingSequence = DOTween.Sequence();

            var comboValueEndPosition = new Vector3(itemStartPosition.x, itemStartPosition.y + _comboValueRecoil,
                itemStartPosition.z);

            _comboValueText.SetText($"+{scoreCombo}");
            _comboValueText.gameObject.SetActive(true);
            _comboValueText.alpha = 255f;

            comboValueShowingSequence
                .AppendCallback(() => _transferer.TransferItem(_comboValueText.transform, itemStartPosition,
                    comboValueEndPosition, _comboValueShowDuration, _comboValueTransferEase))
                .Join(_comboValueText.DOFade(0, _comboValueShowDuration))
                .AppendInterval(_receiveCoinDuration)
                .AppendCallback(() => _comboValueText.gameObject.SetActive(false));
        }
    }
}