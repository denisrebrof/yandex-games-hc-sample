using Purchases.domain.repositories;
using Zenject;

namespace Purchases.domain
{
    public class CoinsPurchaseUseCase
    {

        [Inject] private ICoinsPurchaseRepository coinsPurchaseRepository;
        [Inject] private IBalanceAccessProvider balanceAccessProvider;
        
        public CoinsPurchaseResult ExecutePurchase(long purchaseId)
        {
            var purchasedState = coinsPurchaseRepository.GetPurchasedState(purchaseId);
            if (purchasedState)
                return CoinsPurchaseResult.AlreadyPurchased;

            var cost = coinsPurchaseRepository.GetCost(purchaseId);
            if (!balanceAccessProvider.CanRemove(cost))
                return CoinsPurchaseResult.Failure;
            
            balanceAccessProvider.Remove(cost);
            coinsPurchaseRepository.SetPurchased(purchaseId);
            return CoinsPurchaseResult.Success;
        }
        
        public enum CoinsPurchaseResult
        {
            Success,
            AlreadyPurchased,
            Failure
        }
    }
}