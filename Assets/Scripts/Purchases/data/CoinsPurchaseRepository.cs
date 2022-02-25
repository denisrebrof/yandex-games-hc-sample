using System;
using System.Collections.Generic;
using Purchases.domain.repositories;
using UniRx;
using UnityEngine;
using Zenject;

namespace Purchases.data
{
    public class CoinsPurchaseRepository : ICoinsPurchaseRepository
    {
        [Inject] private IPurchaseEntitiesDao entitiesDao;
        [Inject] private ISavedPurchasedStateDao stateDao;

        private readonly Dictionary<long, ReactiveProperty<bool>> purchasedStateProcessors = new();

        public int GetCost(long purchaseId)
        {
            var entity = entitiesDao.FindById(purchaseId);
            return Mathf.Max(entity.coinsCost, 0);
        }

        public void SetPurchased(long purchaseId)
        {
            stateDao.SetPurchasedState(purchaseId);
            if (!purchasedStateProcessors.ContainsKey(purchaseId)) return;
            var processor = purchasedStateProcessors[purchaseId];
            processor.Value = true;
            processor.Dispose();
            purchasedStateProcessors.Remove(purchaseId);
        }

        public IObservable<bool> GetPurchasedState(long purchaseId)
        {
            if (purchasedStateProcessors.ContainsKey(purchaseId))
                return purchasedStateProcessors[purchaseId];

            var purchasedState = stateDao.GetPurchasedState(purchaseId);
            if (purchasedState)
                return Observable.Return(true);

            var processor = new ReactiveProperty<bool>(false);
            purchasedStateProcessors[purchaseId] = processor;
            return processor;
        }
    }
}