using System;

namespace Ads.presentation.InterstitialAdNavigator
{
    public interface IInterstitalAdNavigator
    {
        IObservable<ShowInterstitialResult> ShowAd();
    }

    public class ShowInterstitialResult
    {
        public bool isSuccess = false;
        public string error;

        public static ShowInterstitialResult Success = new ShowInterstitialResult(true);

        public ShowInterstitialResult(bool isSuccess, string error = "")
        {
            this.isSuccess = isSuccess;
            this.error = error;
        }
    }
}