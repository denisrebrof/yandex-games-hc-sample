namespace Balance.domain.repositories
{
    public interface IRewardRepository
    {
        void Drop();
        void Add(int amount);
        int Get();
    }
}