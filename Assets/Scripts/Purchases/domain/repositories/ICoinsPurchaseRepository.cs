namespace Purchases.domain.repositories
{
    public interface ICoinsPurchaseRepository
    {
        int GetCost(long purchaseId);
        void SetPurchased(long purchaseId);
        bool GetPurchasedState(long purchaseId);
    }
}