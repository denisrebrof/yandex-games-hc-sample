using System;
using Purchases.domain;
using Purchases.domain.model;
using Purchases.domain.repositories;
using UniRx;
using Zenject;
using static Purchases.domain.RewardedVideoPurchaseUseCase.ShowRewardedVideoResult;

namespace Purchases.presentation.ui
{
    public class DefaultPurchaseItemController : PurchaseItem.IPurchaseItemController
    {
        [Inject] private PurchasedStateUseCase purchasedStateUseCase;
        [Inject] private CoinsPurchaseUseCase coinsPurchaseUseCase;
        [Inject] private PurchaseAvailableUseCase purchaseAvailableUseCase;
        [Inject] private RewardedVideoPurchaseUseCase rewardedVideoPurchaseUseCase;

        [Inject] private IPurchaseItemSelectionAdapter selectionAdapter;

        [Inject] private IPurchaseRepository purchaseRepository;

        private ReactiveProperty<bool> purchasedStateFlow = new ReactiveProperty<bool>();

        public void OnItemClick(long purchaseId)
        {
            var purchasedState = purchasedStateUseCase.GetPurchasedState(purchaseId);
            if (purchasedState)
            {
                //Handle Item Selection
                selectionAdapter.SelectItem(purchaseId);
                return;
            }

            switch (purchaseRepository.GetById(purchaseId).Type)
            {
                case PurchaseType.Coins:
                    TryCoinsPurchase(purchaseId);
                    break;
                case PurchaseType.RewardedVideo:
                    TryRewardedVideoPurchase(purchaseId);
                    break;
                case PurchaseType.PassLevelReward:
                    //empty
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            coinsPurchaseUseCase.ExecutePurchase(purchaseId);
        }

        private void TryRewardedVideoPurchase(long purchaseId)
        {
            rewardedVideoPurchaseUseCase.LaunchRewardedVideo(purchaseId,
                delegate(RewardedVideoPurchaseUseCase.ShowRewardedVideoResult result)
                {
                    purchasedStateFlow.Value = result == Purchased;
                });
        }

        private void TryCoinsPurchase(long purchaseId)
        {
            if (!purchaseAvailableUseCase.GetPurchaseAvailable(purchaseId)) return;
            var purchaseResult = coinsPurchaseUseCase.ExecutePurchase(purchaseId);
            purchasedStateFlow.Value = purchaseResult == CoinsPurchaseUseCase.CoinsPurchaseResult.Success;
        }

        public IObservable<bool> GetPurchasedState(long purchaseId)
        {
            if (!purchasedStateFlow.HasValue)
                AddPurchasedStateProperty(purchaseId);

            return purchasedStateFlow;
        }

        private void AddPurchasedStateProperty(long purchaseId)
        {
            var initialState = purchasedStateUseCase.GetPurchasedState(purchaseId);
            purchasedStateFlow.Value = initialState;
        }
        
        public interface IPurchaseItemSelectionAdapter
        {
            public void SelectItem(long purchaseId);
        }
    }
}