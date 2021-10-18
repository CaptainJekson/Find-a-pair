using System;

namespace CJ.FindAPair.Modules.Service.Store
{
    public interface IStoreDriver
    {
        bool PurchaseIfPossible(CurrencyType currencyType, int value);
        bool CanPurchase(CurrencyType currencyType, int value);
        event Action PurchaseCompleted;
        event Action<string> PurchaseFailed;
    }
}