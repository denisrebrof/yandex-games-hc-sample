using System;
using Purchases.domain.model;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace Purchases.presentation.ui
{
    public class PurchaseItem : MonoBehaviour
    {
        [Inject] private IPurchaseItemController itemController;
        public UnityEvent<long> setupEvent = new();
        
        [SerializeField] private GameObject unavaliableStub;
        [SerializeField] private GameObject avaliableStub;
        [SerializeField] private Text name;
        [SerializeField] private Text description;

        private long? purchaseID;

        public void Setup(Purchase purchase)
        {
            purchaseID = purchase.Id;
            setupEvent.Invoke(purchase.Id);

            if (name != null)
                name.text = purchase.Name;
            
            if (description != null)
                description.text = purchase.Description;
            
            itemController
                .GetPurchasedState(purchase.Id)
                .Subscribe(purchased =>
                    Setup(purchase.Id, purchased)
                ).AddTo(this);
        }

        protected virtual void Setup(long purchaseId, bool purchasedState)
        {
            if(unavaliableStub!=null)
                unavaliableStub.SetActive(!purchasedState);
            if(avaliableStub!=null)
                avaliableStub.SetActive(purchasedState);
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