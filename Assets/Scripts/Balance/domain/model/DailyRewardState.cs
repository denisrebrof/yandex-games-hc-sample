using System;

namespace Balance.domain.model
{
    public class DailyRewardState
    {
        public DailyRewardCooldownState CooldownState;
        public int RewardAmount;
        public TimeSpan TimeLeft;

        public DailyRewardState(DailyRewardCooldownState cooldownState, int rewardAmount, TimeSpan timeLeft)
        {
            CooldownState = cooldownState;
            RewardAmount = rewardAmount;
            TimeLeft = timeLeft;
        }
    }
}