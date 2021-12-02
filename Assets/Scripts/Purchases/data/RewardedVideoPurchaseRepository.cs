using Purchases.domain;
using Purchases.domain.repositories;
using Zenject;

namespace Purchases.data
{
    public class RewardedVideoPurchaseRepository: IRewardedVideoPurchaseRepository
    {
        [Inject] private IRewardedVideoWatchDao watchDao;
        [Inject] private IPurchaseEntitiesDao entitiesDao;
        
        public void AddRewardedVideoWatch(long id) => watchDao.AddWatch(id);

        public int GetRewardedVideoCurrentWatchesCount(long id) => watchDao.GetWatches(id);

        public int GetRewardedVideoWatchesCount(long id) => entitiesDao.FindById(id).rewardedVideoCount;

        public interface IRewardedVideoWatchDao
        {
            void AddWatch(long id);
            int GetWatches(long id);
        }
    }
}