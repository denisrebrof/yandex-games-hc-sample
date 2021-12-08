using Balance.domain;
using Purchases.domain;
using Zenject;

namespace Purchases.adapters
{
    public class BalanceAccessProviderAdapter : ExecutePurchaseUseCase.IBalanceAccessProvider
    {
        [Inject] private DecreaseBalanceUseCase decreaseBalanceUseCase;

        public bool CanRemove(int value) => decreaseBalanceUseCase.GetCanDecrease(value);

        public void Remove(int value) => decreaseBalanceUseCase.Decrease(value);
    }
}