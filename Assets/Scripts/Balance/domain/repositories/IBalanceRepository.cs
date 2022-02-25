using System;

namespace Balance.domain.repositories
{
    public interface IBalanceRepository
    {
        IObservable<int> GetBalance();
        void Add(int value);
        void Remove(int value);
    }
}