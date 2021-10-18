using CJ.FindAPair.Modules.Service.Store;
using TMPro;
using UnityEngine;
using Zenject;

namespace CJ.FindAPair.Modules.UI.Windows
{
    public class PlayerResourcesWindow : Window //TODO сделано не унивирсально только для монет
    {
        [SerializeField] private TextMeshProUGUI _goldValueText;

        private ISaver _gameSaver;
        private IStoreDriver _storeDriver;

        [Inject]
        public void Construct(ISaver gameSaver, IStoreDriver storeDriver)
        {
            _gameSaver = gameSaver;
            _storeDriver = storeDriver;
        }

        protected override void OnOpen()
        {
            _storeDriver.PurchaseCompleted += RefreshGold;

            RefreshGold();
        }

        protected override void OnClose()
        {
            _storeDriver.PurchaseCompleted -= RefreshGold;
        }

        private void RefreshGold()
        {
            var saveData = _gameSaver.LoadData();
            _goldValueText.SetText(saveData.ItemsData.Coins.ToString()); 
        }
    }
}