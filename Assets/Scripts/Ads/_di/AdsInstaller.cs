using Ads.InterstitialAdNavigator;
using UnityEngine;
using Zenject;

namespace Ads._di
{
    public class AdsInstaller : MonoInstaller
    {
        [SerializeField] private InterstitialAdNavigatorMuteAudioDecorator muteAudioInterstitialAdNavigatorDecorator;

        public override void InstallBindings()
        {
            Container
                .Bind<IInterstitalAdNavigator>()
#if YANDEX_SDK
                .To<YandexInterstitialAdNavigator>()
#elif VK_SDK
                .To<VKInterstitialAdNavigator>()
#elif POKI_SDK
                .To<PokiInterstitialAdNavigator>()
#else
                .To<DebugLogInterstitialAdNavigator>()
#endif
                .AsSingle()
                .WhenInjectedInto<InterstitialAdNavigatorMuteAudioDecorator>();

            Container
                .Bind<IInterstitalAdNavigator>()
                .To<InterstitialAdNavigatorMuteAudioDecorator>()
                .FromInstance(muteAudioInterstitialAdNavigatorDecorator)
                .AsSingle();
        }
    }
}