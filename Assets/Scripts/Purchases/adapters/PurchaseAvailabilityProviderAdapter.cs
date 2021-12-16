using Balance.domain;
using Purchases.domain;
using Purchases.domain.adapters;
using Zenject;

namespace Purchases.adapters
{
    public class PurchaseAvailabilityProviderAdapter : IPurchaseAvailabilityProvider
    {
        [Inject] private DecreaseBalanceUseCase decreaseBalanceUseCase;

        public bool GetPurchaseAvailable(int price) => decreaseBalanceUseCase.GetCanDecrease(price);
    }
}