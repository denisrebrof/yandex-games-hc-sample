using System;
using System.Collections.Generic;
using Purchases.domain.repositories;
using UniRx;
using Zenject;

namespace Purchases.data
{
    public class RewardedVideoPurchaseRepository : IRewardedVideoPurchaseRepository
    {
        [Inject] private IRewardedVideoWatchDao watchDao;
        [Inject] private IPurchaseEntitiesDao entitiesDao;

        private readonly Dictionary<long, ReactiveProperty<int>> watchesCountProcessors = new();

        public void AddRewardedVideoWatch(long id) => watchDao.AddWatch(id);

        public IObservable<int> GetRewardedVideoCurrentWatchesCount(long id)
        {
            if (watchesCountProcessors.ContainsKey(id))
                return watchesCountProcessors[id];

            var watchesCount = watchDao.GetWatches(id);
            var processor = new ReactiveProperty<int>(watchesCount);
            watchesCountProcessors[id] = processor;
            return processor;
        }

        public int GetRewardedVideoWatchesCount(long id) => entitiesDao.FindById(id).rewardedVideoCount;

        public interface IRewardedVideoWatchDao
        {
            void AddWatch(long id);
            int GetWatches(long id);
        }
    }
}