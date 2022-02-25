using System;

namespace Purchases.domain.repositories
{
    public interface IRewardedVideoPurchaseRepository
    {
        void AddRewardedVideoWatch(long id);
        IObservable<int> GetRewardedVideoCurrentWatchesCount(long id);
        int GetRewardedVideoWatchesCount(long id);
    }
}