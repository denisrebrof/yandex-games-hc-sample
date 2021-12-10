using Balance.domain.repositories;
using Zenject;

namespace Balance.domain
{
    public class DecreaseBalanceUseCase
    {
        [Inject] private IBalanceRepository repository;

        public bool GetCanDecrease(int value) => repository.GetBalance() > value;

        // bool
        public DecreaseBalanceResult Decrease(int value)
        {
            if (!GetCanDecrease(value))
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