using System;
using Purchases.domain.model;
using Purchases.domain.repositories;
using UniRx;
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

        public IObservable<Boolean> GetPurchasedState(long purchaseId)
        {
            var type = repository.GetById(purchaseId).Type;
            switch (type)
            {
                case PurchaseType.Coins:
                    return coinsPurchaseRepository.GetPurchasedState(purchaseId);
                case PurchaseType.RewardedVideo:
                    var currentWatchesFlow = videoPurchaseRepository.GetRewardedVideoCurrentWatchesCount(purchaseId);
                    var requiredWatches = videoPurchaseRepository.GetRewardedVideoWatchesCount(purchaseId);
                    return currentWatchesFlow.Select(currentWatches => currentWatches >= requiredWatches);
                case PurchaseType.PassLevelReward:
                    var levelId = passLevelPurchaseRepository.GetLevelId(purchaseId);
                    return levelPassedStateProvider.GetLevelPassedState(levelId);
                default:
                    return Observable.Return(false);
            }
        }
    }
}