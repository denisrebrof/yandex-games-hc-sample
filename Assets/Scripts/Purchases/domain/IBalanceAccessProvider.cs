using System;

namespace Purchases.domain
{
    public interface IBalanceAccessProvider
    {
        IObservable<bool> CanRemove(int value);
        IObservable<bool> Remove(int value);
    }
}