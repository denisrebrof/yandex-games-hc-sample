using UnityEngine;

namespace Purchases.data.dao
{
    public class PlayerPrefsRewardedVideoWatchesDao: RewardedVideoPurchaseRepository.IRewardedVideoWatchDao
    {
        private const string PrefsKeyPrefix = "RewardedVideoWatches";
        public void AddWatch(long id)
        {
            var prefKey = GetPrefKey(id);
            var currentWatches = GetWatches(id);
            PlayerPrefs.SetInt(prefKey, currentWatches + 1);
        }

        public int GetWatches(long id)
        {
            var prefKey = GetPrefKey(id);
            return PlayerPrefs.GetInt(prefKey, 0);
        }
        
        private static string GetPrefKey(long purchaseId) => $"{PrefsKeyPrefix}_{purchaseId}";
    }
}