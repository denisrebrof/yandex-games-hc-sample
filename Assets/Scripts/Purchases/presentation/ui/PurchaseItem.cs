using System;
using Purchases.domain.model;
using UniRx;
using UnityEngine;
using Zenject;

namespace Purchases.presentation.ui
{
    public class PurchaseItem : MonoBehaviour
    {
        [Inject] private IPurchaseItemController itemController;

        [SerializeField] private GameObject unavaliableStub;

        private long? purchaseID;

        public void Setup(Purchase purchase)
        {
            purchaseID = purchase.Id;
            itemController
                .GetPurchasedState(purchase.Id)
                .Subscribe(purchased =>
                    Setup(purchase.Id, purchased)
                ).AddTo(this);
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

        public interface IPurchaseItemController
        {
            void OnItemClick(long purchaseId);
            IObservable<bool> GetPurchasedState(long purchaseId);
        }
    }
}