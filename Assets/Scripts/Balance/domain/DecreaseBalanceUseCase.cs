using System;
using Balance.domain.repositories;
using UniRx;
using Zenject;

namespace Balance.domain
{
    public class DecreaseBalanceUseCase
    {
        [Inject] private IBalanceRepository repository;

        public IObservable<bool> GetCanDecrease(int value) => repository
            .GetBalance()
            .Select(balance => balance >= value);

        // bool
        public IObservable<DecreaseBalanceResult> Decrease(int value) => GetCanDecrease(value)
            .Take(1)
            .Select(canDecrease =>
                DecreaseBalance(canDecrease, value)
            );

        private DecreaseBalanceResult DecreaseBalance(bool canDecrease, int value)
        {
            if (!canDecrease)
                return DecreaseBalanceResult.LowBalance;

            repository.Remove(value);
            return DecreaseBalanceResult.Success;
        }

        public enum DecreaseBalanceResult
        {
            Success,
            LowBalance,
            UnexpectedFailure
        }
    }
}