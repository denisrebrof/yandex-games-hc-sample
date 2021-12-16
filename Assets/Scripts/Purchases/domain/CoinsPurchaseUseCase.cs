using Purchases.domain.adapters;
using Purchases.domain.repositories;
using Zenject;

namespace Purchases.domain
{
    public class CoinsPurchaseUseCase
    {
        [Inject] private ICoinsPurchaseRepository coinsPurchaseRepository;
        [Inject] private IPurchasePaymentHandler purchasePaymentHandler;

        public CoinsPurchaseResult ExecutePurchase(long purchaseId)
        {
            var purchasedState = coinsPurchaseRepository.GetPurchasedState(purchaseId);
            if (purchasedState)
                return CoinsPurchaseResult.AlreadyPurchased;

            var cost = coinsPurchaseRepository.GetCost(purchaseId);
            var paymentResult = purchasePaymentHandler.ExecutePayment(cost);
            if (paymentResult != IPurchasePaymentHandler.PurchasePaymentResult.Success)
                return CoinsPurchaseResult.Failure;

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