using System;
using Balance.domain.model;
using Balance.domain.repositories;
using UniRx;
using static Balance.domain.model.DailyRewardCooldownState;
using Zenject;

namespace Balance.domain
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