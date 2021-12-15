using System;
using Balance.domain.model;
using Balance.domain.repositories;
using Zenject;

namespace Balance.domain
{
    public class CollectDailyRewardUseCase
    {
        [Inject] private DailyRewardCooldownStateUseCase dailyRewardCooldownState;
        [Inject] private IDailyRewardRepository dailyRewardRepository;
        [Inject] private IBalanceRepository balanceRepository;

        public CollectRewardResult Collect()
        {
            switch (dailyRewardCooldownState.GetRewardCooldownState())
            {
                case DailyRewardCooldownState.Prepared:
                    var amount = dailyRewardRepository.GetRewardAmount();
                    balanceRepository.Add(amount);
                    dailyRewardRepository.SetLastCollectTime(DateTime.Now);
                    return CollectRewardResult.Success;
                case DailyRewardCooldownState.OnCooldown:
                    return CollectRewardResult.NotPrepared;
                case DailyRewardCooldownState.Disabled:
                    return CollectRewardResult.DailyRewardDisabled;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public enum CollectRewardResult
        {
            Success,
            NotPrepared,
            DailyRewardDisabled
        }
    }
}