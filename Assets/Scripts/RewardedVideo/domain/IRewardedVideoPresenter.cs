using System;
using RewardedVideo.domain.model;

namespace RewardedVideo.domain
{
    public interface IRewardedVideoPresenter
    {
        void ShowRewardedVideo(Action<ShowRewardedVideoResult> callback);
    }
}