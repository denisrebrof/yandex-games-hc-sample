using System;
using Balance.domain;
using Purchases.domain;
using UniRx;
using Zenject;

namespace Purchases.adapters
{
    public class BalanceAccessProviderAdapter : IBalanceAccessProvider
    {
        [Inject] private DecreaseBalanceUseCase decreaseBalanceUseCase;

        public IObservable<bool> CanRemove(int value) => decreaseBalanceUseCase.GetCanDecrease(value);

        public IObservable<bool> Remove(int value) => decreaseBalanceUseCase.Decrease(value).Select(result => result == DecreaseBalanceUseCase.DecreaseBalanceResult.Success) ;
    }
}