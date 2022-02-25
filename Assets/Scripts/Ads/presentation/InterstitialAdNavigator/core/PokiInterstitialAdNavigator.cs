using System;
using UniRx;

namespace Ads.presentation.InterstitialAdNavigator.core
{
    public class PokiInterstitialAdNavigator : IInterstitalAdNavigator
    {
#if POKI_SDK
        [Inject] private PokiUnitySDK sdk;
#endif

        public IObservable<ShowInterstitialResult> ShowAd()
        {
#if POKI_SDK
            if (sdk.adsBlocked())
                return Observable.Return(new ShowInterstitialResult(false, "adsBlocked"));

            if (sdk.isShowingAd)
                return Observable.Return(new ShowInterstitialResult(false, "isShowingAd"));

            return Observable.Create((IObserver<ShowInterstitialResult> observer) =>
                {
                    Action callback = () => {
                        observer.OnNext(ShowInterstitialResult.Success);
                        observer.OnCompleted();
                    };
                    var callbackDelegate = new PokiUnitySDK.CommercialBreakDelegate(callback);
                    callbackDelegate += () => sdk.commercialBreakCallBack -= callbackDelegate;
                    sdk.commercialBreakCallBack += callbackDelegate;
                    
                    sdk.commercialBreak();
                    //Double check for editor case
                    if (!sdk.isShowingAd)
                    {
                        observer.OnNext(new ShowInterstitialResult(false, "!isShowingAd"));
                        observer.OnCompleted();
                        sdk.commercialBreakCallBack -= callbackDelegate;
                    }
                        
                    return Disposable.Create(() => { });
                }
            );
#endif
            return Observable.Return(ShowInterstitialResult.Success);
        }
    }
}