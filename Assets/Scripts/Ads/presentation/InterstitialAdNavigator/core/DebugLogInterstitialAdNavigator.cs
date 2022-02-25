using System;
using System.Collections;
using UniRx;
using UnityEngine;

namespace Ads.presentation.InterstitialAdNavigator.core
{
    public class DebugLogInterstitialAdNavigator: IInterstitalAdNavigator
    {
        public IObservable<ShowInterstitialResult> ShowAd()
        {
            Debug.Log("Debug Show interstitial");
            return Observable
                .FromCoroutine(() => WaitForRealSeconds(5))
                .Select(_ => ShowInterstitialResult.Success);
        }
 
        IEnumerator WaitForRealSeconds (float seconds) {
            float startTime = Time.realtimeSinceStartup;
            while (Time.realtimeSinceStartup-startTime < seconds) {
                yield return null;
            }
        }
    }
}