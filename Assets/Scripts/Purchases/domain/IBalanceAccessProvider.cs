namespace Purchases.domain
{
    public interface IBalanceAccessProvider
    {
        bool CanRemove(int value);
        void Remove(int value);
    }
}