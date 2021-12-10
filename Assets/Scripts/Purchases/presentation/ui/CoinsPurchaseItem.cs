using Purchases.domain;
using Purchases.domain.model;
using Purchases.domain.repositories;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Purchases.presentation.ui
{
    public class CoinsPurchaseItem: PurchaseItem
    {
        [Inject] private ICoinsPurchaseRepository coinsPurchaseRepository;
        
        [SerializeField] private GameObject cost;
        [SerializeField] private Text costText;

        protected override void Setup(long purchaseId, bool purchasedState)
        {
            base.Setup(purchaseId, purchasedState);
            costText.text = coinsPurchaseRepository.GetCost(purchaseId).ToString();
            cost.SetActive(!purchasedState);
        }

        public class Factory : PlaceholderFactory<CoinsPurchaseItem> { }
    }
}