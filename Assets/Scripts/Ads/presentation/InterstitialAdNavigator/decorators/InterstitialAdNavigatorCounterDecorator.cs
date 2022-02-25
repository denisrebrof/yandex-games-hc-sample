using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Ads.presentation.InterstitialAdNavigator.decorators
{
    public class InterstitialAdNavigatorCounterDecorator : MonoBehaviour, IInterstitalAdNavigator
    {
        [Inject] private IInterstitalAdNavigator adNavigator;
        //TODO: replace with di
        private readonly IInterstitialShowIntervalProvider intervalProvider = new NoIntervalInterstitialShowIntervalProvider();
        private int invokeTimes;
        private int showInterval = 1;

        private void Start()
        {
            intervalProvider
                .GetShowInterval()
                .Subscribe(interval => showInterval = interval)
                .AddTo(this);
        }

        public IObservable<ShowInterstitialResult> ShowAd()
        {
            if (showInterval == 0)
                return Observable.Return(new ShowInterstitialResult(false, "zero show interval"));

            invokeTimes++;
            if (invokeTimes < showInterval)
                Observable.Return(new ShowInterstitialResult(false, "period not reached"));

            invokeTimes = 0;
            return adNavigator.ShowAd();
        }

        public interface IInterstitialShowIntervalProvider
        {
            public IObservable<int> GetShowInterval();
        }
    }
    
    public class NoIntervalInterstitialShowIntervalProvider: InterstitialAdNavigatorCounterDecorator.IInterstitialShowIntervalProvider
    {
        public IObservable<int> GetShowInterval() => Observable.Return(1);
    }
}