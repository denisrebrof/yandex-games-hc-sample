using System;
using RewardedVideo.domain;
using RewardedVideo.domain.model;
using UniRx;
using UnityEngine;

namespace RewardedVideo.presentation
{
    public class YandexRewardedVideoPresenter : MonoBehaviour, IRewardedVideoPresenter
    {
        [SerializeField] private string placement;

        private readonly YandexSDK sdk = YandexSDK.instance;

        private readonly ReactiveProperty<ShowRewardedVideoResult> showResult = new ReactiveProperty<ShowRewardedVideoResult>();

        private void Awake()
        {
            sdk.onRewardedAdReward += i => showResult.SetValueAndForceNotify(ShowRewardedVideoResult.Success);
            sdk.onRewardedAdClosed += i => showResult.SetValueAndForceNotify(ShowRewardedVideoResult.Closed);
            sdk.onRewardedAdError += i => showResult.SetValueAndForceNotify(ShowRewardedVideoResult.Error);
        }

        public void ShowRewardedVideo(Action<ShowRewardedVideoResult> callback)
        {
            sdk.ShowRewarded(placement);
            showResult.First().Subscribe(callback).AddTo(this);
        }
    }
}