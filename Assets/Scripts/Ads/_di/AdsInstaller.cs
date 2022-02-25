using Ads.presentation.InterstitialAdNavigator;
using Ads.presentation.InterstitialAdNavigator.core;
using Ads.presentation.InterstitialAdNavigator.decorators;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Ads._di
{
    public class AdsInstaller : MonoInstaller
    {
        [SerializeField] private InterstitialAdNavigatorCounterDecorator dieCounterNavigator;
        [SerializeField] private InterstitialAdNavigatorCounterDecorator levelLoadedCounterNavigator;

        [SerializeField] private InterstitialAdNavigatorMuteAudioDecorator muteAudioInterstitialAdNavigatorDecorator;

        public override void InstallBindings()
        {
            Container
                .Bind<IInterstitalAdNavigator>()
#if YANDEX_SDK
                .To<YandexInterstitialAdNavigator>()
                .AsSingle()
                .WhenInjectedInto<YandexInterstitialNavigatorHitsDecorator>();
                
            Container
                .Bind<IInterstitalAdNavigator>()
                .To<YandexInterstitialNavigatorHitsDecorator>()
#elif VK_SDK
                .To<VKInterstitialAdNavigator>()
#elif POKI_SDK
                .To<PokiInterstitialAdNavigator>()
#elif CRAZY_SDK
                .To<CrazyInterstitialAdNavigator>()
#else
                .To<DebugLogInterstitialAdNavigator>()
#endif
                .AsSingle()
                .WhenInjectedInto<InterstitialAdNavigatorAnalyticsDecorator>();

            Container
                .Bind<IInterstitalAdNavigator>()
                .To<InterstitialAdNavigatorAnalyticsDecorator>()
                .FromNew()
                .AsSingle()
                .WhenInjectedInto<InterstitialAdNavigatorMuteAudioDecorator>();

            Container
                .Bind<IInterstitalAdNavigator>()
                .To<InterstitialAdNavigatorMuteAudioDecorator>()
                .FromInstance(muteAudioInterstitialAdNavigatorDecorator)
                .AsSingle()
                .WhenInjectedInto<InterstitialAdNavigatorCounterDecorator>();
        }
    }
}