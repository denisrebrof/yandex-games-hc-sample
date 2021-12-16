using System;
using Balance.domain.repositories;
using DailyReward.domain.model;
using DailyReward.domain.repositories;
using UniRx;
using Zenject;
using static DailyReward.domain.model.DailyRewardCooldownState;

namespace DailyReward.domain
{
    public class DailyRewardStateUseCase
    {
        [Inject] private IDailyRewardRepository dailyRewardRepository;
        [Inject] private DailyRewardCooldownStateUseCase dailyRewardCooldownState;

        public IObservable<DailyRewardState> GetRewardState()
        {
            var cooldown = dailyRewardRepository.GetCollectCooldown();
            var amount = dailyRewardRepository.GetRewardAmount();

            return Observable
                .Timer(DateTimeOffset.Now, TimeSpan.FromSeconds(1))
                .Select(l =>
                {
                    var nextCollectTime = dailyRewardRepository.GetLastCollectTime().Add(cooldown);
                    return GetRewardState(cooldown, amount, nextCollectTime);
                });
        }
        
        public DailyRewardState GetCurrentRewardState()
        {
            var cooldown = dailyRewardRepository.GetCollectCooldown();
            var amount = dailyRewardRepository.GetRewardAmount();
            var nextCollectTime = dailyRewardRepository.GetLastCollectTime().Add(cooldown);
            return GetRewardState(cooldown, amount, nextCollectTime);
        }

        private DailyRewardState GetRewardState(TimeSpan cooldown, int amount, DateTime nextCollectTime)
        {
            var cooldownState = dailyRewardCooldownState.GetRewardCooldownState();
            var timeLeft = cooldownState == Prepared ? DateTime.Now - nextCollectTime : TimeSpan.Zero;
            return new DailyRewardState(cooldownState, amount, timeLeft);
        }
    }
}