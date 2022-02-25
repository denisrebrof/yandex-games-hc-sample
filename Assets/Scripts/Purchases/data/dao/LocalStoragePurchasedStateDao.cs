using Plugins.FileIO;

namespace Purchases.data.dao
{
    public class LocalStoragePurchasedStateDao : ISavedPurchasedStateDao
    {
        private const string PrefsKeyPrefix = "RewardedVideoWatches";

        public bool GetPurchasedState(long purchaseId)
        {
            var prefKey = GetPrefKey(purchaseId);
            return LocalStorageIO.GetInt(prefKey, 0) > 0;
        }

        public void SetPurchasedState(long purchaseId)
        {
            var prefKey = GetPrefKey(purchaseId);
            LocalStorageIO.SetInt(prefKey, 1);
        }

        private static string GetPrefKey(long purchaseId) => $"{PrefsKeyPrefix}_{purchaseId}";
    }
}