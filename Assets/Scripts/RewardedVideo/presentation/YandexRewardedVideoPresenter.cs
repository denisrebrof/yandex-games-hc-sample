using System;
using System.Collections;
using System.Collections.Generic;
using RewardedVideo.domain;
using RewardedVideo.domain.model;
using UniRx;
using UnityEngine;

namespace RewardedVideo.presentation
{
    public class YandexRewardedVideoPresenter : MonoBehaviour, IRewardedVideoPresenter
    {
        [SerializeField] private string placement;

        private YandexSDK sdk;

        private readonly ReactiveProperty<ShowRewardedVideoResult> showResult = new ReactiveProperty<ShowRewardedVideoResult>();

        private void Start()
        {
            sdk = YandexSDK.instance;
            if(sdk == null)
                return;
            sdk.onRewardedAdReward += i => showResult.SetValueAndForceNotify(ShowRewardedVideoResult.Success);
            sdk.onRewardedAdClosed += i => showResult.SetValueAndForceNotify(ShowRewardedVideoResult.Closed);
            sdk.onRewardedAdError += i => showResult.SetValueAndForceNotify(ShowRewardedVideoResult.Error);
        }

        public void ShowRewardedVideo(Action<ShowRewardedVideoResult> callback)
        {
            sdk.ShowRewarded(placement);
            if (sdk != null) showResult.First().Subscribe(callback).AddTo(this);
            else StartCoroutine(DebugRewardedVideoStub());
        }

        private IEnumerator DebugRewardedVideoStub()
        {
            yield return new WaitForSeconds(1f);
            showResult.SetValueAndForceNotify(ShowRewardedVideoResult.Success);
        }
    }
}