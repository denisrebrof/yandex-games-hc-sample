using System;
using JetBrains.Annotations;
using Purchases.domain;
using Purchases.domain.model;
using UniRx;
using UnityEngine;
using Zenject;

namespace Purchases.presentation.ui
{
    public class PurchaseItem : MonoBehaviour
    {
        [Inject] private IPurchaseItemController itemController;
        [Inject] private PurchasedStateUseCase purchaseStateUseCase;

        [SerializeField] private GameObject unavaliableStub;

        private long? purchaseID;

        [CanBeNull] private IDisposable purchasedStateDisposable;

        public void Setup(Purchase purchase)
        {
            purchaseID = purchase.Id;
            var purchased = purchaseStateUseCase.GetPurchasedState(purchase.Id);
            purchasedStateDisposable = itemController
                .GetPurchasedState(purchase.Id)
                .Subscribe(purchased =>
                    Setup(purchase.Id, purchased)
                );
        }

        protected virtual void Setup(long purchaseId, bool purchasedState)
        {
            unavaliableStub.SetActive(!purchasedState);
        }

        public void Click()
        {
            if (!purchaseID.HasValue) return;
            itemController.OnItemClick(purchaseID.Value);
        }

        private void OnDisable() => purchasedStateDisposable?.Dispose();

        private void OnDestroy() => purchasedStateDisposable?.Dispose();

        public interface IPurchaseItemController
        {
            void OnItemClick(long purchaseId);
            IObservable<bool> GetPurchasedState(long purchaseId);
        }
    }
}