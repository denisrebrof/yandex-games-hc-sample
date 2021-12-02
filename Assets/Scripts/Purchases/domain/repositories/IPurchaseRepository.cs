using System.Collections.Generic;
using Purchases.domain.model;

namespace Purchases.domain.repositories
{
    public interface IPurchaseRepository
    {
        public List<Purchase> GetPurchases();
        public Purchase GetById(long id);
    }
}