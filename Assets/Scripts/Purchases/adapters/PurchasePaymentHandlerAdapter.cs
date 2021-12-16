using System;
using Balance.domain;
using Balance.domain.repositories;
using Purchases.domain.adapters;
using Zenject;
using static Balance.domain.DecreaseBalanceUseCase;
using static Purchases.domain.adapters.IPurchasePaymentHandler;

namespace Purchases.adapters
{
    public class PurchasePaymentHandlerAdapter: IPurchasePaymentHandler
    {
        [Inject] private DecreaseBalanceUseCase decreaseBalanceUseCase;

        public IPurchasePaymentHandler.PurchasePaymentResult ExecutePayment(int price)
        {
            var decreaseResult = decreaseBalanceUseCase.Decrease(price);
            switch (decreaseResult)
            {
                case DecreaseBalanceResult.Success: return PurchasePaymentResult.Success;
                default: return PurchasePaymentResult.Failure;
            }
        }
    }
}