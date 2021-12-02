using Purchases.domain.repositories;
using Zenject;

namespace Purchases.domain
{
    public class ExecutePurchaseUseCase
    {

        [Inject] private ICoinsPurchaseRepository coinsPurchaseRepository;
        [Inject] private IBalanceAccessProvider balanceAccessProvider;
        
        public PurchaseResult ExecutePurchase(long purchaseId)
        {
            var purchasedState = coinsPurchaseRepository.GetPurchasedState(purchaseId);
            if (purchasedState)
                return PurchaseResult.AlreadyPurchased;

            var cost = coinsPurchaseRepository.GetCost(purchaseId);
            if (!balanceAccessProvider.CanRemove(cost))
                return PurchaseResult.Failure;
            
            balanceAccessProvider.Remove(cost);
            coinsPurchaseRepository.SetPurchased(purchaseId);
            return PurchaseResult.Success;
        }
        
        public enum PurchaseResult
        {
            Success,
            AlreadyPurchased,
            Failure
        }

        public interface IBalanceAccessProvider
        {
            bool CanRemove(int value);
            void Remove(int value);
        }
    }
}