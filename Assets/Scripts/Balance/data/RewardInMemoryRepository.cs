using Balance.domain.repositories;

namespace Balance.data
{
    public class RewardInMemoryRepository : IRewardRepository
    {
        private int reward;

        public void Drop()
        {
            reward = 0;
        }

        public void Add(int amount)
        {
            if (amount <= 0) return;
            reward += amount;
        }

        public int Get() => reward;
    }
}