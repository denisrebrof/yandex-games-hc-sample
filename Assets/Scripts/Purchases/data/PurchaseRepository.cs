using System.Collections.Generic;
using System.Linq;
using Purchases.domain.model;
using Purchases.domain.repositories;
using Zenject;

namespace Purchases.data
{
    public class PurchaseRepository : IPurchaseRepository
    {
        [Inject] private IPurchaseEntitiesDao entitiesDao;

        public List<Purchase> GetPurchases() => entitiesDao
            .GetLevelEntities()
            .Select(entity => entity.toPurchase())
            .ToList();

        public Purchase GetById(long id) => entitiesDao
            .GetLevelEntities()
            .First(entity => entity.Id == id)
            .toPurchase();
    }
}