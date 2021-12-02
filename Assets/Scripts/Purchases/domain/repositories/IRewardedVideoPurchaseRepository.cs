namespace Purchases.domain.repositories
{
    public interface IRewardedVideoPurchaseRepository
    {
        void AddRewardedVideoWatch(long id);
        int GetRewardedVideoCurrentWatchesCount(long id);
        int GetRewardedVideoWatchesCount(long id);
    }
}