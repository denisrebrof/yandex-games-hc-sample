using Levels.domain;

namespace Levels.data
{
    public class LastRewardInMemoryRepository : ILastRewardRepository
    {
        private int reward;

        public void Set(int amount) => reward = amount;

        public int Get() => reward;
    }
}