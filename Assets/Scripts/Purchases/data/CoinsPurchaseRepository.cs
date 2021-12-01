using System.Linq;
using Purchases.domain;
using UnityEngine;
using Zenject;

namespace Purchases.data
{
    public class CoinsPurchaseRepository : ICoinsPurchaseRepository
    {
        [Inject] private IPurchaseEntitiesDao entitiesDao;

        public int GetCost(long purchaseId)
        {
            var entity = entitiesDao.FindById(purchaseId);
            return Mathf.Max(entity.coinsCost, 0);
        }
    }
}