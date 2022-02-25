using System;
using Purchases.domain.repositories;
using UniRx;
using Zenject;

namespace Purchases.domain
{
    public class CoinsPurchaseUseCase
    {
        [Inject] private ICoinsPurchaseRepository coinsPurchaseRepository;
        [Inject] private IBalanceAccessProvider balanceAccessProvider;

        public IObservable<CoinsPurchaseResult> ExecutePurchase(long purchaseId) => coinsPurchaseRepository
            .GetPurchasedState(purchaseId)
            .Take(1)
            .SelectMany(state =>
                state ? Observable.Return(CoinsPurchaseResult.AlreadyPurchased) : ExecuteNewPurchase(purchaseId)
            );

        private IObservable<CoinsPurchaseResult> ExecuteNewPurchase(long purchaseId)
        {
            var cost = coinsPurchaseRepository.GetCost(purchaseId);
            return balanceAccessProvider
                .CanRemove(cost)
                .Take(1)
                .SelectMany(enoughBalance =>
                    {
                        if (!enoughBalance) return Observable.Return(CoinsPurchaseResult.Failure);
                        return balanceAccessProvider.Remove(cost).Select(result =>
                            {
                                if (!result)
                                    return CoinsPurchaseResult.Failure;

                                coinsPurchaseRepository.SetPurchased(purchaseId);
                                return CoinsPurchaseResult.Success;
                            }
                        );
                    }
                );
        }

        public enum CoinsPurchaseResult
        {
            Success,
            AlreadyPurchased,
            Failure
        }
    }
}