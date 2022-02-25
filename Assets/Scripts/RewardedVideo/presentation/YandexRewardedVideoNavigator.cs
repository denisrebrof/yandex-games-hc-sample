using System;
using RewardedVideo.domain;
using RewardedVideo.domain.model;
using UnityEngine;
using Zenject;

namespace RewardedVideo.presentation
{
    public class YandexRewardedVideoNavigator: IRewardedVideoNavigator
    {
        

        public IObservable<ShowRewardedVideoResult> ShowRewardedVideo()
        {
            throw new NotImplementedException();
        }
    }
}