using CJ.FindAPair.Modules.Service.Store;
using CJ.FindAPair.Utility;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace CJ.FindAPair.Modules.UI.Windows
{
    public class PlayerResourcesWindow : Window //TODO сделано не унивирсально только для монет
    {
        [SerializeField] private float _durationCounter;
        
        [SerializeField] private TextMeshProUGUI _goldValueText;
        [SerializeField] private TextMeshProUGUI _diamondValueText;

        private ISaver _gameSaver;
        private IStoreDriver _storeDriver;

        private int _coinValue;
        private int _diamondValue;

        [Inject]
        public void Construct(ISaver gameSaver, IStoreDriver storeDriver)
        {
            _gameSaver = gameSaver;
            _storeDriver = storeDriver;
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
    }
}