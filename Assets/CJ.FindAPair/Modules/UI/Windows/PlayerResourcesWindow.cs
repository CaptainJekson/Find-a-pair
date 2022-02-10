using CJ.FindAPair.Modules.CoreGames.Configs;
using CJ.FindAPair.Modules.Service.Store;
using CJ.FindAPair.Modules.UI.Windows.Base;
using CJ.FindAPair.Utility;
using TMPro;
using UnityEngine;
using Zenject;

namespace CJ.FindAPair.Modules.UI.Windows
{
    public class PlayerResourcesWindow : Window
    {
        [SerializeField] private float _durationCounter;
        
        [SerializeField] private TextMeshProUGUI _goldValueText;
        [SerializeField] private TextMeshProUGUI _diamondValueText;
        [SerializeField] private TextMeshProUGUI _energyValueText;
        [SerializeField] private TextMeshProUGUI _energyCooldownTimerText;

        private ISaver _gameSaver;
        private IStoreDriver _storeDriver;
        private EnergyCooldownHandler _energyCooldownHandler;
        private GameSettingsConfig _gameSettingsConfig;

        private int _coinValue;
        private int _diamondValue;

        [Inject]
        public void Construct(ISaver gameSaver, IStoreDriver storeDriver, EnergyCooldownHandler energyCooldownHandler, 
            GameSettingsConfig gameSettingsConfig)
        {
            _gameSaver = gameSaver;
            _storeDriver = storeDriver;
            _energyCooldownHandler = energyCooldownHandler;
            _gameSettingsConfig = gameSettingsConfig;
        }

        protected override void OnOpen()
        {
            _storeDriver.PurchaseCompleted += SmoothRefreshValues;
            SetValues();
        }
        
        protected override void OnClose()
        {
            _storeDriver.PurchaseCompleted -= SmoothRefreshValues;
        }

        private void Update()
        {
            CheckEnergyValue();
        }

        private void SetValues()
        {
            var saveData = _gameSaver.LoadData();
            
            _coinValue = saveData.ItemsData.Coins;
            _diamondValue = saveData.ItemsData.Diamond;

            _goldValueText.SetText(_coinValue.ToString());
            _diamondValueText.SetText(_diamondValue.ToString());
        }
        
        private void SmoothRefreshValues()
        {
            var saveData = _gameSaver.LoadData();
            
            var newCoinValue = saveData.ItemsData.Coins;
            var newDiamondValue = saveData.ItemsData.Diamond;

            _goldValueText.ChangeOfNumericValueForText(_coinValue, newCoinValue, _durationCounter);
            _diamondValueText.ChangeOfNumericValueForText(_diamondValue, newDiamondValue, _durationCounter);
        }

        private void CheckEnergyValue()
        {
            var saveData = _gameSaver.LoadData();

            if (saveData.ItemsData.Energy < _gameSettingsConfig.MaxEnergyValue)
            {
                _energyCooldownTimerText.SetText(_energyCooldownHandler.ShowEnergyCooldownTimeInterval());
                _energyCooldownHandler.TryIncreaseScore();
            }
            else if(saveData.ItemsData.Energy >= _gameSettingsConfig.MaxEnergyValue)
            {
                _energyCooldownTimerText.SetText("max");
            }
            
            _energyValueText.SetText(saveData.ItemsData.Energy.ToString());
        }
    }
}