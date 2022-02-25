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
        [Inject] private PurchaseEntityConverter converter;

        public List<Purchase> GetPurchases() => entitiesDao
            .GetLevelEntities()
            .Select(converter.GetPurchaseFromEntity)
            .ToList();

        public Purchase GetById(long id)
        {
            var purchaseEntity = entitiesDao
                .GetLevelEntities()
                .First(entity => entity.Id == id);
            
            return converter.GetPurchaseFromEntity(purchaseEntity);
        }
    }
}