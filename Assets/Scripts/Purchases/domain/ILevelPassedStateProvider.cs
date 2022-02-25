using System;

namespace Purchases.domain
{
    public interface ILevelPassedStateProvider
    {
        IObservable<bool> GetLevelPassedState(long levelId);
    }
}