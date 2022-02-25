using System;
using UniRx;
using Zenject;

namespace Ads.presentation.InterstitialAdNavigator.core
{
    public class YandexInterstitialAdNavigator : IInterstitalAdNavigator
    {
        [Inject] private Plugins.YSDK.YandexSDK instance;
        public IObservable<ShowInterstitialResult> ShowAd()
        {
#if YANDEX_SDK && !UNITY_EDITOR
            var interstitialShownObservable = Observable.FromEvent(
                handler => instance.onInterstitialShown += handler,
                handler => instance.onInterstitialShown -= handler
            ).Select(_ => ShowInterstitialResult.Success);

            var interstitialFailedObservable = Observable.FromEvent<string>(
                handler => instance.onInterstitialFailed += handler,
                handler => instance.onInterstitialFailed -= handler
            ).Select((error) => new ShowInterstitialResult(false, error));

            return Observable
                .Merge(interstitialShownObservable, interstitialFailedObservable)
                .Take(1)
                .DoOnSubscribe(() => instance.ShowInterstitial());
#endif
            return Observable.Return(ShowInterstitialResult.Success);
        }
    }
}