using System;
using Purchases.domain.model;
using UnityEngine;

namespace Purchases.data.model
{
    [Serializable]
    public class PurchaseEntity
    {
        public long Id;
        public string RuName;
        public string RuDescription;
        public string EnName;
        public string EnDescription;
        public long passRewardLevelId = -1;
        public int coinsCost = 0;
        public int rewardedVideoCount = 1;
        public PurchaseType Type;
        public Sprite image;
    }
}