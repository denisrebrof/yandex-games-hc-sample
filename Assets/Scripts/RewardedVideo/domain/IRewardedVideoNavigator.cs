using System;
using RewardedVideo.domain.model;

namespace RewardedVideo.domain
{
    public interface IRewardedVideoNavigator
    {
        IObservable<ShowRewardedVideoResult> ShowRewardedVideo();
    }
}