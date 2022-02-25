using System;
using UniRx;
using Zenject;
#if YANDEX_SDK && !UNITY_EDITOR
using Plugins.YSDK;
#endif

namespace Ads.presentation.InterstitialAdNavigator.decorators
{
    public class YandexInterstitialNavigatorHitsDecorator : IInterstitalAdNavigator
    {
        [Inject] private IInterstitalAdNavigator adNavigator;
#if YANDEX_SDK && !UNITY_EDITOR
        [Inject] private YandexSDK instance;
#endif

        public IObservable<ShowInterstitialResult> ShowAd()
        {
#if YANDEX_SDK && !UNITY_EDITOR
            instance.AddHit("TryShowInterstitial");
#endif
            return adNavigator.ShowAd().Do(HandleResult);
        }

        private void HandleResult(ShowInterstitialResult result)
        {
#if YANDEX_SDK && !UNITY_EDITOR
            var hit = "ShowInterstitial/";
            hit += result.isSuccess ? "Success" : "NotSuccess";
            instance.AddHit(hit);
#endif
        }
    }
}