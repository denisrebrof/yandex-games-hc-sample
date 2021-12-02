using Purchases.domain.repositories;
using UnityEngine;
using Zenject;

namespace Purchases.data
{
    public class CoinsPurchaseRepository : ICoinsPurchaseRepository
    {
        [Inject] private IPurchaseEntitiesDao entitiesDao;
        [Inject] private ISavedPurchasedStateDao stateDao;

        public int GetCost(long purchaseId)
        {
            var entity = entitiesDao.FindById(purchaseId);
            return Mathf.Max(entity.coinsCost, 0);
        }

        public void SetPurchased(long purchaseId) => stateDao.SetPurchasedState(purchaseId);
        public bool GetPurchasedState(long purchaseId) => stateDao.GetPurchasedState(purchaseId);
    }
}