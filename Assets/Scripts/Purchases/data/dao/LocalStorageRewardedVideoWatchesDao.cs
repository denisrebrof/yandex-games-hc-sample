using Plugins.FileIO;
using UnityEngine;

namespace Purchases.data.dao
{
    public class LocalStorageRewardedVideoWatchesDao: RewardedVideoPurchaseRepository.IRewardedVideoWatchDao
    {
        private const string PrefsKeyPrefix = "RewardedVideoWatches";
        public void AddWatch(long id)
        {
            var prefKey = GetPrefKey(id);
            var currentWatches = GetWatches(id);
            LocalStorageIO.SetInt(prefKey, currentWatches + 1);
        }

        public int GetWatches(long id)
        {
            var prefKey = GetPrefKey(id);
            return LocalStorageIO.GetInt(prefKey, 0);
        }
        
        private static string GetPrefKey(long purchaseId) => $"{PrefsKeyPrefix}_{purchaseId}";
    }
}