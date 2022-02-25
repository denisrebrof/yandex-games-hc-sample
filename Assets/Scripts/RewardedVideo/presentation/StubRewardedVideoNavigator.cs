using System;
using RewardedVideo.domain;
using RewardedVideo.domain.model;
using UniRx;

namespace RewardedVideo.presentation
{
    public class StubRewardedVideoNavigator: IRewardedVideoNavigator
    {
        public IObservable<ShowRewardedVideoResult> ShowRewardedVideo()
        {
            return Observable.Return(ShowRewardedVideoResult.Success);
        }
    }
}