using System;

namespace CJ.FindAPair.Modules.Service.Store
{
    public class StoreDriver : IStoreDriver
    {
        private ISaver _gameSaver;

        public event Action PurchaseCompleted;
        public event Action<string> PurchaseFailed;
        
        public StoreDriver(ISaver gameSaver)
        {
            _gameSaver = gameSaver;
        }
        
        public void PurchaseIfPossible(CurrencyType currencyType, int value)
        {
            var resourceValue = GetResourceValue(currencyType);
            
            if (resourceValue < value)
            {
                PurchaseFailed?.Invoke("[Not enough coins]");
            }
            else
            {
                DecreaseResourceValue(currencyType, value);    
                PurchaseCompleted?.Invoke();
            }  
        }

        private int GetResourceValue(CurrencyType currencyType)
        {
            var saveData = _gameSaver.LoadData();

            return currencyType switch
            {
                CurrencyType.Coins => saveData.ItemsData.Coins,
                CurrencyType.Diamonds => saveData.ItemsData.Diamond,
                _ => throw new ArgumentOutOfRangeException(nameof(currencyType), currencyType, null)
            };
        }
        
        private void DecreaseResourceValue(CurrencyType currencyType, int value)
        {
            var saveData = _gameSaver.LoadData();

            switch (currencyType)
            {
                case CurrencyType.Coins:
                    saveData.ItemsData.Coins -= value;
                    break;
                case CurrencyType.Diamonds:
                    saveData.ItemsData.Diamond -= value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(currencyType), currencyType, null);
            }
        }
    }
}