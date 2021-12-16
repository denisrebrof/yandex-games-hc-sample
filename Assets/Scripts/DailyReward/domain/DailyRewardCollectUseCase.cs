using System;
using Balance.domain;
using Balance.domain.repositories;
using DailyReward.domain.model;
using DailyReward.domain.repositories;
using Zenject;

namespace DailyReward.domain
{
    public class DailyRewardCollectUseCase
    {
        [Inject] private DailyRewardCooldownStateUseCase dailyRewardCooldownState;
        [Inject] private IDailyRewardRepository dailyRewardRepository;
        [Inject] private IDailyRewardCollectHandler rewardCollectHandler;

        public CollectRewardResult Collect()
        {
            switch (dailyRewardCooldownState.GetRewardCooldownState())
            {
                case DailyRewardCooldownState.Prepared:
                    var amount = dailyRewardRepository.GetRewardAmount();
                    rewardCollectHandler.Handle(amount);
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
        
        public interface IDailyRewardCollectHandler
        {
            public void Handle(int amount);
        }

        public enum CollectRewardResult
        {
            Success,
            NotPrepared,
            DailyRewardDisabled
        }
    }
}