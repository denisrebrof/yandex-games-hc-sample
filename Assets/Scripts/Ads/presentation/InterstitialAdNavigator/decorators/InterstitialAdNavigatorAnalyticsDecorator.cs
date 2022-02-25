using System;
using Analytics;
using Analytics.adapter;
using Analytics.ads;
using Analytics.ads.placement;
using UniRx;
using Zenject;

namespace Ads.presentation.InterstitialAdNavigator.decorators
{
    public class InterstitialAdNavigatorAnalyticsDecorator : IInterstitalAdNavigator
    {
        [Inject] private IInterstitalAdNavigator target;
        [Inject] private AnalyticsAdapter analytics;

        private IAdPlacement placement = new SimpleAdPlacement("undefined");
        private AdProvider provider = AdProviderProvider.CurrentProvider;

        public IObservable<ShowInterstitialResult> ShowAd()
        {
            SendEvent(AdAction.Request);
            return target.ShowAd().Do(HandleShowResult);
        }

        private void HandleShowResult(ShowInterstitialResult result)
        {
            if (!result.isSuccess)
                OnFailed(result.error);
            else
                OnShown();
        }

        private void OnShown() => SendEvent(AdAction.Show);

        private void OnFailed(string error) => SendEvent(AdAction.Failure);

        private void SendEvent(AdAction action)
        {
            analytics.SendAdEvent(action, AdType.Interstitial, provider, placement);
        }
    }
}