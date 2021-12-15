using System;
using Purchases.domain.repositories;
using Zenject;

namespace Purchases.domain
{
    public class RewardedVideoPurchaseUseCase
    {
        [Inject] private IRewardedVideoPurchasePresenterAdapter rewardedVideoPurchasePresenterAdapter;
        [Inject] private IRewardedVideoPurchaseRepository rewardedVideoPurchaseRepository;
        [Inject] private PurchasedStateUseCase purchasedStateUseCase;

        public enum ShowRewardedVideoResult
        {
            Purchased,
            InProgress,
            Failure
        }

        public void LaunchRewardedVideo(long purchaseId, Action<ShowRewardedVideoResult> callback)
        {
            rewardedVideoPurchasePresenterAdapter?.ShowInterstitial(delegate(bool result)
            {
                if (!result)
                {
                    callback.Invoke(ShowRewardedVideoResult.Failure);
                    return;
                }

                rewardedVideoPurchaseRepository.AddRewardedVideoWatch(purchaseId);
                var completed = purchasedStateUseCase.GetPurchasedState(purchaseId);
                callback.Invoke(completed ? ShowRewardedVideoResult.Purchased : ShowRewardedVideoResult.InProgress);
            });
        }

        public interface IRewardedVideoPurchasePresenterAdapter
        {
            public void ShowInterstitial(Action<bool> successCallback);
        }
    }
}