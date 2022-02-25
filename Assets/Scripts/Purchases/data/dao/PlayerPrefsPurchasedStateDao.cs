using UnityEngine;

namespace Purchases.data.dao
{
    public class PlayerPrefsPurchasedStateDao : ISavedPurchasedStateDao
    {
        private const string PrefsKeyPrefix = "RewardedVideoWatches";

        public bool GetPurchasedState(long purchaseId)
        {
            var prefKey = GetPrefKey(purchaseId);
            return PlayerPrefs.GetInt(prefKey, 0) > 0;
        }

        public void SetPurchasedState(long purchaseId)
        {
            var prefKey = GetPrefKey(purchaseId);
            PlayerPrefs.SetInt(prefKey, 1);
        }

        private static string GetPrefKey(long purchaseId) => $"{PrefsKeyPrefix}_{purchaseId}";
    }
}