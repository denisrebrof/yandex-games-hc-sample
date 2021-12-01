using System;
using Purchases.domain.model;

namespace Purchases.domain
{
    public interface IPassLevelRewardPurchasesRepository
    {
        public bool HasForLevel(long levelId);
        long GetLevelId(long purchaseId);
        public Purchase GetForLevel(long levelId);
    }
}