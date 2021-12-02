namespace Purchases.data
{
    public interface ISavedPurchasedStateDao
    {
        bool GetPurchasedState(long purchaseId);
        void SetPurchasedState(long purchaseId);
    }
}