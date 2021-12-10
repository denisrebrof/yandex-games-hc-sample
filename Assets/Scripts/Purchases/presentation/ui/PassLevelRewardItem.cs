using Purchases.domain.repositories;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Purchases.presentation.ui
{
    public class PassLevelRewardItem: PurchaseItem
    {

        [SerializeField] private GameObject labelRoot;
        [SerializeField] private Text levelText;
        protected override void Setup(long purchaseId, bool purchasedState)
        {
            base.Setup(purchaseId, purchasedState);
            levelText.text = passLevelRewardPurchasesRepository.GetLevelId(purchaseId).ToString();
            labelRoot.SetActive(!purchasedState);
        }

        public class Factory : PlaceholderFactory<PassLevelRewardItem> { }
        
        public interface ILevelNumberProvider
        {
            int GetLevelNumber(long purchaseId);
        }
    }
}