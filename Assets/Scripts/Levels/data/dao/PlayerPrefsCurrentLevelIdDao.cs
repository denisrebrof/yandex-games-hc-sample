using System;
using UnityEngine;

namespace Levels.data.dao
{
    public class PlayerPrefsCurrentLevelIdDao : CurrentLevelRepository.ICurrentLevelIdDao
    {
        private const string PrefsKeyPrefix = "CurrentLevelId";
        
        public bool HasCurrentLevelId() => PlayerPrefs.HasKey(PrefsKeyPrefix);

        public long GetCurrentLevelId()
        {
            var storedLevelIdData = PlayerPrefs.GetString(PrefsKeyPrefix);
            return Convert.ToInt64(storedLevelIdData);
        }

        public void SetCurrentLevelId(long id) => PlayerPrefs.SetString(PrefsKeyPrefix, id.ToString());
    }
}