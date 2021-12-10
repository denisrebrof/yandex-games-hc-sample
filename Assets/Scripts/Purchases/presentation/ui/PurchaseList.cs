using Purchases.domain.model;
using Purchases.domain.repositories;
using UnityEngine;
using Zenject;

namespace Purchases.presentation.ui
{
    public class PurchaseList : MonoBehaviour
    {
        [SerializeField] private Transform listRoot;
        [SerializeReference] private IPurchaseItemFactory purchaseItemFactory;
        [Inject] private IPurchaseRepository purchasesRepository;

        private void Awake()
        {
            if (listRoot == null)
                listRoot = transform;

            purchasesRepository.GetPurchases().ForEach(CreateItem);
        }

        private void CreateItem(Purchase purchase)
        {
            var item = purchaseItemFactory.Create();
            item.transform.SetParent(listRoot);
            item.Setup(purchase);
        }
    }
}