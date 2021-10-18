using System;

namespace CJ.FindAPair.Modules.Service.Store
{
    public interface IStoreDriver
    {
        void PurchaseIfPossible(CurrencyType currencyType, int value);
        event Action PurchaseCompleted;
        event Action<string> PurchaseFailed;
    }
}