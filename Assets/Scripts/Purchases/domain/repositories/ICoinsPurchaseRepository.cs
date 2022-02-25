using System;

namespace Purchases.domain.repositories
{
    public interface ICoinsPurchaseRepository
    {
        int GetCost(long purchaseId);
        void SetPurchased(long purchaseId);
        IObservable<bool> GetPurchasedState(long purchaseId);
    }
}