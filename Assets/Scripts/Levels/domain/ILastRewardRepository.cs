namespace Levels.domain
{
    public interface ILastRewardRepository
    {
        void Set(int amount);
        int Get();
    }
}