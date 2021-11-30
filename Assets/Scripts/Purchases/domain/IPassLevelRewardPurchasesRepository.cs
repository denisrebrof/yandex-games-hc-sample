using System;
using Purchases.domain.model;

namespace Purchases.domain
{
    public interface IPassLevelRewardPurchasesRepository
    {
        public bool HasForLevel(long levelId);
        public Purchase GetForLevel(long levelId);
    }
}