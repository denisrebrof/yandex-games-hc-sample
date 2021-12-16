using System;
using DailyReward.domain.repositories;
using UniRx;
using UnityEngine;
using Zenject;

namespace DailyReward.data
{
    public class PlayerPrefsDailyRewardRepository : IDailyRewardRepository
    {
        [Inject] private IDailyRewardDao dailyRewardDao;

        private const string LastCollectPrefsKey = "DailyRewardLastCollectTime";

        private BehaviorSubject<DateTime> lastCollectionTimeSubject;

        public PlayerPrefsDailyRewardRepository()
        {
            lastCollectionTimeSubject = new BehaviorSubject<DateTime>(GetLastCollectTime());
        }

        public int GetRewardAmount() => dailyRewardDao.GetRewardAmount();

        public DateTime GetLastCollectTime()
        {
            var defaultLastCollectTime = DateTime.Now - dailyRewardDao.GetRewardCooldown();
            var defaultTimeStringValue = defaultLastCollectTime.ToBinary().ToString();
            var lastCollectTimeStringValue = PlayerPrefs.GetString(LastCollectPrefsKey, defaultTimeStringValue);
            return DateTime.FromBinary(Convert.ToInt64(lastCollectTimeStringValue));
        }

        public IObservable<DateTime> GetLastCollectTimeFlow() => lastCollectionTimeSubject;

        public void SetLastCollectTime(DateTime time)
        {
            var lastCollectTimeStringValue = time.ToBinary().ToString();
            PlayerPrefs.SetString(LastCollectPrefsKey, lastCollectTimeStringValue);
            lastCollectionTimeSubject.OnNext(time);
        }

        public TimeSpan GetCollectCooldown() => dailyRewardDao.GetRewardCooldown();

        public interface IDailyRewardDao
        {
            int GetRewardAmount();
            TimeSpan GetRewardCooldown();
        }
    }
}