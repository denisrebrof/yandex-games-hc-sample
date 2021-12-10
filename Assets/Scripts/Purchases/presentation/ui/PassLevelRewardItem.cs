﻿using Purchases.domain.repositories;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Purchases.presentation.ui
{
    public class PassLevelRewardItem: PurchaseItem
    {
        [Inject] private ILevelNumberProvider levelNumberProvider;

        [SerializeField] private GameObject labelRoot;
        [SerializeField] private Text levelText;
        protected override void Setup(long purchaseId, bool purchasedState)
        {
            base.Setup(purchaseId, purchasedState);
            levelText.text = levelNumberProvider.GetLevelNumber(purchaseId).ToString();
            labelRoot.SetActive(!purchasedState);
        }

        public class Factory : PlaceholderFactory<PassLevelRewardItem> { }
        
        public interface ILevelNumberProvider
        {
            int GetLevelNumber(long purchaseId);
        }
    }
}