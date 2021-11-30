using System.Collections.Generic;
using Purchases.domain.model;

namespace Purchases.domain
{
    public interface IPurchaseRepository
    {
        public List<Purchase> GetPurchases();
        public void Purchase(long purchaseId);
    }
}