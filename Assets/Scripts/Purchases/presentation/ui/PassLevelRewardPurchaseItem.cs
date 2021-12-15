using Purchases.domain.repositories;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Purchases.presentation.ui
{
    public class PassLevelRewardPurchaseItem: PurchaseItem
    {
        [Inject] private ILevelNumberProvider levelNumberProvider;

        [SerializeField] private GameObject labelRoot;
        [SerializeField] private Text levelText;
        protected override void Setup(long purchaseId, bool purchasedState)
        {
            base.Setup(purchaseId, purchasedState);
            levelText.text = "LVL " + levelNumberProvider.GetLevelNumber(purchaseId);
            labelRoot.SetActive(!purchasedState);
        }

        public class Factory : PlaceholderFactory<PassLevelRewardPurchaseItem> { }
        
        public interface ILevelNumberProvider
        {
            int GetLevelNumber(long purchaseId);
        }
    }
}