using UnityEngine;

namespace Levels.data.dao
{
    public class PlayerPrefsLevelCompletedStateDao : ILevelCompletedStateDao
    {
        private const string PrefsKeyPrefix = "LevelCompletedState";

        public bool IsCompleted(long levelId)
        {
            var prefKey = GetPrefKey(levelId);
            var prefValue = PlayerPrefs.GetInt(prefKey, (int) CompletedState.NotCompleted);
            var prefstate = (CompletedState)prefValue;
            return prefstate == CompletedState.Completed;
        }

        public void SetCompleted(long levelId)
        {
            var prefKey = GetPrefKey(levelId);
            PlayerPrefs.SetInt(prefKey, (int) CompletedState.Completed);
        }

        private static string GetPrefKey(long levelId) => $"{PrefsKeyPrefix}_{levelId}";

        private enum CompletedState
        {
            NotCompleted,
            Completed
        }
    }
}