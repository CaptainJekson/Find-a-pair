using System;
using CJ.FindAPair.Modules.Meta.Configs;
using CJ.FindAPair.Modules.Meta.Themes;
using CJ.FindAPair.Modules.Service;
using CJ.FindAPair.Modules.Service.Store;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CJ.FindAPair.Modules.UI.Slots
{
    public class ThemeSlot : MonoBehaviour
    {
        [SerializeField] private Image _themeIcon;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _descriptionText;

        [SerializeField] private Button _buyButton;
        [SerializeField] private Image _requeredLevelPanel;
        [SerializeField] private Button _selectButton;
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private Image _currencyImage;
        [SerializeField] private TextMeshProUGUI _requiredLevelText;
        [SerializeField] private Toggle _randomChangeThemeToggle;

        [SerializeField] private Sprite _coinSprite;
        [SerializeField] private Sprite _diamondSprite;

        private ThemesSelector _themesSelector;
        private IStoreDriver _storeDriver;

        private Action _refreshThemeWindowAction;
        private CurrencyType _currencyType;
        private int _price;

        public string ThemeId { get; private set; }

        private void Awake()
        {
            _buyButton.onClick.AddListener(ThemePurchase);
            _selectButton.onClick.AddListener(ThemeSelect);
        }

        public void Init(ThemesSelector themesSelector, IStoreDriver storeDriver, Action refreshThemeWindowAction)
        {
            _themesSelector = themesSelector;
            _storeDriver = storeDriver;
            _refreshThemeWindowAction = refreshThemeWindowAction;
        }

        public void SetData(ThemeConfig themeConfig)
        {
            ThemeId = themeConfig.Id;
            _themeIcon.sprite = themeConfig.FacesSprites[0];
            _nameText.text = themeConfig.Name;
            _descriptionText.text = themeConfig.Description;
            _priceText.text = themeConfig.Price.ToString();
            _price = themeConfig.Price;
            _currencyType = themeConfig.CurrencyType;
            RefreshBuyButton();

            _currencyImage.sprite = themeConfig.CurrencyType switch
            {
                CurrencyType.Coins => _coinSprite,
                CurrencyType.Diamonds => _diamondSprite,
                CurrencyType.RealMoney => _diamondSprite,
                _ => throw new ArgumentOutOfRangeException()
            };

            if (themeConfig.IsOpensLevel)
            {
                _requeredLevelPanel.gameObject.SetActive(true);
                _requiredLevelText.text = $"Opens in {themeConfig.RequiredLevel} lvl";
            }
        }

        public void EnableChangeThemeToggle()
        {
            _randomChangeThemeToggle.gameObject.SetActive(true);
        }

        public void EnableSelectButton(bool isEnabled)
        {
            _selectButton.gameObject.SetActive(isEnabled);
        }

        public void RefreshBuyButton()
        {
            _buyButton.interactable = _storeDriver.CanPurchase(_currencyType, _price);
        }

        private void ThemePurchase()
        {
            if (_storeDriver.PurchaseIfPossible(_currencyType, _price))
            {
                _themesSelector.AddOpenedTheme(ThemeId);
                _refreshThemeWindowAction?.Invoke();
            }
        }

        private void ThemeSelect()
        {
            _themesSelector.ChangeTheme(ThemeId);
            _refreshThemeWindowAction?.Invoke();
        }
    }
}