namespace Levels.data.dao
{
    public class HardcodedDefaultLevelIdDao: CurrentLevelRepository.IDefaultLevelIdDao
    {
        private long defaultLevelId = 0;
        public long GetDefaultLevelId() => defaultLevelId;
    }
}