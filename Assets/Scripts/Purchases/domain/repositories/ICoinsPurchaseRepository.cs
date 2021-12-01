using System;

namespace Purchases.domain
{
    public interface ICoinsPurchaseRepository
    {
        int GetCost(long purchaseId);
    }
}