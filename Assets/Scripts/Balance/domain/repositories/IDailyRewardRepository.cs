using System;

namespace Balance.domain.repositories
{
    public interface IDailyRewardRepository
    {
        int GetRewardAmount();
        TimeSpan GetCollectCooldown();
        DateTime GetLastCollectTime();
        
        IObservable<DateTime> GetLastCollectTimeFlow();
        void SetLastCollectTime(DateTime time);
    }
}