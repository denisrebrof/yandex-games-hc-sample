using System;
using UniRx;
using Zenject;

namespace Ads.InterstitialAdNavigator
{
    public class VKInterstitialAdNavigator : IInterstitalAdNavigator
    {
        [Inject] private VKSDK instance;

        public IObservable<ShowInterstitialResult> ShowAd()
        {
#if VK_SDK
            var interstitialShownObservable = Observable.FromEvent(
                handler => instance.onInterstitialShown += handler,
                handler => instance.onInterstitialShown -= handler
            ).Select((_) => ShowInterstitialResult.Success);

            var interstitialFailedObservable = Observable.FromEvent<string>(
                handler => instance.onInterstitialFailed += handler,
                handler => instance.onInterstitialFailed -= handler
            ).Select((error) => new ShowInterstitialResult(false, error));

            return Observable
                .Merge(interstitialShownObservable, interstitialFailedObservable)
                .First()
                .DoOnSubscribe( () => instance.ShowInterstitial());
#endif
            return Observable.Return(ShowInterstitialResult.Success);
        }
    }
}