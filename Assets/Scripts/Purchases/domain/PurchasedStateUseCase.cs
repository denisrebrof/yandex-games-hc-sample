using System;
using Purchases.domain.model;
using Purchases.domain.repositories;
using Zenject;

namespace Purchases.domain
{
    public class PurchasedStateUseCase
    {
        [Inject] private IPurchaseRepository repository;
        [Inject] private ICoinsPurchaseRepository coinsPurchaseRepository;
        [Inject] private IRewardedVideoPurchaseRepository videoPurchaseRepository;
        [Inject] private IPassLevelRewardPurchasesRepository passLevelPurchaseRepository;
        [Inject] private ILevelPassedStateProvider levelPassedStateProvider;

        public Boolean GetPurchasedState(long purchaseId)
        {
            var type = repository.GetById(purchaseId).Type;
            switch (type)
            {
                case PurchaseType.Coins:
                    return coinsPurchaseRepository.GetPurchasedState(purchaseId);
                case PurchaseType.RewardedVideo:
                    var currentWatches = videoPurchaseRepository.GetRewardedVideoCurrentWatchesCount(purchaseId);
                    var requiredWatches = videoPurchaseRepository.GetRewardedVideoWatchesCount(purchaseId);
                    return currentWatches >= requiredWatches;
                case PurchaseType.PassLevelReward:
                    var levelId = passLevelPurchaseRepository.GetLevelId(purchaseId);
                    return levelPassedStateProvider.GetLevelPassedState(levelId);
                default:
                    return false;
            }
        }

        public interface ILevelPassedStateProvider
        {
            Boolean GetLevelPassedState(long levelId);
        }
    }
}