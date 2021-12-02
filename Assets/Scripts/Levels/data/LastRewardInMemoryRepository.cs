using Levels.domain;

namespace Levels.data
{
    public class LastRewardInMemoryRepository : ILastRewardRepository
    {
        private int reward = 0;

        public void Set(int amount) => reward = amount;

        public int Get() => reward;
    }
}