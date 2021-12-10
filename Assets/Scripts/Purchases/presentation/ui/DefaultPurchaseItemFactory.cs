using Purchases.domain.model;
using UnityEngine;
using UnityEngine.UI;

namespace Purchases.presentation.ui
{
    public class DefaultPurchaseItemFactory: MonoBehaviour, IPurchaseItemFactory
    {
        [SerializeField] private CoinsPurchaseItem coinsPurchaseItemPrefab;
        [SerializeField] private PassLevelRewardItem passLevelRewardItemPrefab;
        // [SerializeField] private CoinsPurchaseItem coinsPurchaseItemPrefab;
        public PurchaseItem Create(PurchaseType type)
        {
            
        }
    }
}