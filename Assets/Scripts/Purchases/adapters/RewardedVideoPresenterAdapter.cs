using System;
using Purchases.domain;
using RewardedVideo.domain;
using RewardedVideo.domain.model;
using Zenject;

namespace Purchases.adapters
{
    public class RewardedVideoPurchasePresenterAdapter: RewardedVideoPurchaseUseCase.IRewardedVideoPurchasePresenterAdapter
    {
        [Inject] private IRewardedVideoPresenter rewardedVideoPresenter;
        public void ShowInterstitial(Action<bool> successCallback)
        {
            rewardedVideoPresenter.ShowRewardedVideo(result => successCallback.Invoke(result==ShowRewardedVideoResult.Success));
        }
    }
}