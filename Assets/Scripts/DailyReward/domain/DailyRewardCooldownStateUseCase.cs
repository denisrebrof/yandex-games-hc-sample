using System;
using Balance.domain.repositories;
using DailyReward.domain.model;
using DailyReward.domain.repositories;
using Zenject;
using static DailyReward.domain.model.DailyRewardCooldownState;

namespace DailyReward.domain
{
    public class DailyRewardCooldownStateUseCase
    {
        [Inject] private IDailyRewardRepository dailyRewardRepository;
        
        public DailyRewardCooldownState GetRewardCooldownState()
        {
            var cooldown = dailyRewardRepository.GetCollectCooldown();
            var nextCollectTime = dailyRewardRepository.GetLastCollectTime().Add(cooldown);
            var onCooldown = DateTime.Now < nextCollectTime;
            return onCooldown ? OnCooldown : Prepared;
        }
    }
}