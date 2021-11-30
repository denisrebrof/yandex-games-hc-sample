namespace Balance.domain
{
    public interface IBalanceRepository
    {
        int GetBalance();
        void Add(int value);
        void Remove(int value);
    }
}