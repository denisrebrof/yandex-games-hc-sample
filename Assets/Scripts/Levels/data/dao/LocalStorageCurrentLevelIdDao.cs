using System;
using Plugins.FileIO;

namespace Levels.data.dao
{
    public class LocalStorageCurrentLevelIdDao : CurrentLevelRepository.ICurrentLevelIdDao
    {
        private const string PrefsKeyPrefix = "CurrentLevelId";

        public bool HasCurrentLevelId() => LocalStorageIO.HasKey(PrefsKeyPrefix);

        public long GetCurrentLevelId()
        {
            var storedLevelIdData = LocalStorageIO.GetString(PrefsKeyPrefix);
            return Convert.ToInt64(storedLevelIdData);
        }

        public void SetCurrentLevelId(long id) => LocalStorageIO.SetString(PrefsKeyPrefix, id.ToString());
    }
}