using Purchases.domain.model;
using Purchases.domain.repositories;
using Zenject;

namespace Purchases.domain
{
    public class PurchaseAvailableUseCase
    {
        [Inject] private PurchasedStateUseCase purchasedStateUseCase;
        [Inject] private IPurchaseRepository repository;
        [Inject] private IBalanceAccessProvider balance;
        [Inject] private ICoinsPurchaseRepository coinsPurchaseRepository;
        [Inject] private IRewardedVideoPurchaseRepository videoPurchaseRepository;
        
        public bool GetPurchaseAvailable(long purchaseId)
        {
            if (purchasedStateUseCase.GetPurchasedState(purchaseId))
                return false;

            var purchase = repository.GetById(purchaseId);
            switch (purchase.Type)
            {
                case PurchaseType.Coins:
                    var cost = coinsPurchaseRepository.GetCost(purchaseId);
                    return balance.CanRemove(cost);
                case PurchaseType.RewardedVideo:
                    var currentWatches = videoPurchaseRepository.GetRewardedVideoCurrentWatchesCount(purchaseId);
                    var requiredWatches = videoPurchaseRepository.GetRewardedVideoWatchesCount(purchaseId);
                    return currentWatches + 1 >= requiredWatches;
                case PurchaseType.PassLevelReward:
                    return false;
                default:
                    return false;
            }
        }
    }
}