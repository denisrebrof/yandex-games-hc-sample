﻿using Levels.presentation.ui;
using Purchases.presentation.ui;
using UnityEngine;
using Zenject;

namespace Purchases._di
{
    public class PurchasesPresentationInstaller : MonoInstaller
    {
        [SerializeField] private CoinsPurchaseItem coinsPurchaseItemPrefab;
        [SerializeField] private PassLevelRewardPurchaseItem passLevelRewardItemPrefab;
        [SerializeField] private RewardedVideoPurchaseItem rewardedVideoPurchaseItemPrefab;

        public override void InstallBindings()
        {
            //Presentation
            //UI
            Container.Bind<LevelItem.ILevelItemController>().To<DefaultLevelItemController>().AsSingle();
            //Item Factories
            Container.BindFactory<CoinsPurchaseItem, CoinsPurchaseItem.Factory>()
                .FromComponentInNewPrefab(coinsPurchaseItemPrefab);
            Container.BindFactory<PassLevelRewardPurchaseItem, PassLevelRewardPurchaseItem.Factory>()
                .FromComponentInNewPrefab(passLevelRewardItemPrefab);
            Container.BindFactory<RewardedVideoPurchaseItem, RewardedVideoPurchaseItem.Factory>()
                .FromComponentInNewPrefab(rewardedVideoPurchaseItemPrefab);
            Container.Bind<IPurchaseItemFactory>().To<DefaultPurchaseItemFactory>().AsSingle();
        }
    }
}