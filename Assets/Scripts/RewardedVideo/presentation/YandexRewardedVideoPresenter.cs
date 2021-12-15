using System;
using RewardedVideo.domain;
using RewardedVideo.domain.model;
using UnityEngine;

namespace RewardedVideo.presentation
{
    public class YandexRewardedVideoPresenter: MonoBehaviour, IRewardedVideoPresenter
    {
        public void ShowRewardedVideo(Action<ShowRewardedVideoResult> callback)
        {
            throw new NotImplementedException();
        }
    }
}