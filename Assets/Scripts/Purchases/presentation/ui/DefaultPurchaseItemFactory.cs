using System;
using Purchases.domain.model;
using Zenject;

namespace Purchases.presentation.ui
{
    public class DefaultPurchaseItemFactory : IPurchaseItemFactory
    {
        [Inject] private CoinsPurchaseItem.Factory coinsPurchaseItemFactory;

        [Inject] private PassLevelRewardPurchaseItem.Factory passLevelRewardItemFactory;

        [Inject] private RewardedVideoPurchaseItem.Factory rewardedVideoItemFactory;

        public PurchaseItem Create(PurchaseType type)
        {
            PurchaseItem item;
            switch (type)
            {
                case PurchaseType.Coins:
                    item = coinsPurchaseItemFactory.Create();
                    break;
                case PurchaseType.RewardedVideo:
                    item = rewardedVideoItemFactory.Create();
                    break;
                case PurchaseType.PassLevelReward:
                    item = passLevelRewardItemFactory.Create();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            return item;
        }
    }
}