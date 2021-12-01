using System;
using Purchases.domain.model;

namespace Purchases.data.model
{
    [Serializable]
    public class PurchaseEntity
    {
        public long Id;
        public string Name;
        public long passRewardLevelId = -1;
        public int coinsCost = 0;
        public int rewardedVideoCount = 1;
        public PurchaseType Type;

        public PurchaseEntity(long id, string name, long passRewardLevelId, int coinsCost, int rewardedVideoCount = -1, PurchaseType type = PurchaseType.Coins)
        {
            Id = id;
            Name = name;
            this.passRewardLevelId = passRewardLevelId;
            this.coinsCost = coinsCost;
            this.rewardedVideoCount = rewardedVideoCount;
            Type = type;
        }
        
        public Purchase toPurchase() => new Purchase(
            Id,
            Name,
            Type
        );
    }
}