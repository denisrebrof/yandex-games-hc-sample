using System;
using System.Collections.Generic;
using System.Linq;
using Purchases.data.model;
using Purchases.domain;
using Purchases.domain.model;
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
    }
}