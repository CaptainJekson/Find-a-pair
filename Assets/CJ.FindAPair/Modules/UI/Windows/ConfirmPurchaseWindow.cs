using System;
using CJ.FindAPair.Modules.Service;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CJ.FindAPair.Modules.UI.Windows
{
    public class ConfirmPurchaseWindow : Window
    {
        [SerializeField] private Button _buyButton;
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private Image _currencyIcon;

        [SerializeField] private Sprite _coinIcon;
        [SerializeField] private Sprite _diamondIcon;

        private Action _purchaseAction;
        private CurrencyType _currencyType;
        private int _price;
        private AudioController _audioController;

        [Inject]
        public void Construct(AudioController audioController)
        {
            _audioController = audioController;
        }
        
        protected override void Init()
        {
            _buyButton.onClick.AddListener(MakePurchase);
        }

        protected override void OnOpen()
        {
            _audioController.PlaySound(_audioController.AudioClipsCollection.WindowOpenSound);
            _priceText.text = _price.ToString();

            _currencyIcon.sprite = _currencyType switch
            {
                CurrencyType.Coins => _coinIcon,
                CurrencyType.Diamonds => _diamondIcon,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public void SetData(CurrencyType currencyType, int price, Action purchaseAction)
        {
            _currencyType = currencyType;
            _price = price;
            _purchaseAction = purchaseAction;
        }

        private void MakePurchase()
        {
            _purchaseAction?.Invoke();
            Close();
        }
    }
}
