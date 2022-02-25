using System;
using Purchases.domain;
using RewardedVideo.domain;
using RewardedVideo.domain.model;
using UniRx;
using Zenject;

namespace Purchases.adapters
{
    public class RewardedVideoPresenterAdapter : RewardedVideoPurchaseUseCase.IRewardedVideoPurchasePresenterAdapter
    {
        [Inject] private IRewardedVideoNavigator rewardedVideoNavigator;

        public IObservable<bool> ShowRewarded() => rewardedVideoNavigator
            .ShowRewardedVideo()
            .Select(result => result == ShowRewardedVideoResult.Success);
    }
}