// ReSharper disable once RedundantUsingDirective
using System;
// ReSharper disable once RedundantUsingDirective
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CrazyGames
{
    public class CrazyAds : Singleton<CrazyAds>
    {
        public delegate void AdBreakCompletedCallback();

        private AdBreakCompletedCallback onCompletedAdBreak;

        public delegate void AdErrorCallback();

        private AdErrorCallback onAdError;

        private bool _isRunningAd;
        private bool origRunInBackground;
        private float origTimeScale;
        private List<CrazyBanner> banners;

        private void Awake()
        {
            CrazySDK.Instance.AddEventListener(CrazySDKEvent.adError, ev => AdError(((AdErrorEvent)ev).error));
            CrazySDK.Instance.AddEventListener(CrazySDKEvent.adFinished, ev => AdFinished());
            CrazySDK.Instance.AddEventListener(CrazySDKEvent.adStarted, ev => AdStarted());
            CrazySDK.Instance.AddEventListener(CrazySDKEvent.inGameBannerError,
                ev => BannerError(((BannerErrorEvent)ev).error));
            CrazySDK.Instance.AddEventListener(CrazySDKEvent.inGameBannerRendered,
                ev => BannerRendered(((BannerRenderedEvent)ev).id));
            banners = new List<CrazyBanner>();
        }

        #region adbreak

        private bool IsRunningAd
        {
            get { return _isRunningAd; }
            set
            {
                _isRunningAd = value;

                var options = FindObjectOfType<CrazyAdsOptions>();
                AudioListener.volume = _isRunningAd ? 0 : 1;

                if (options)
                    foreach (var obj in options.activeDuringAdObjs)
                        obj.SetActive(_isRunningAd);

                if (!options || options.freezeTimeDuringBreak)
                    Time.timeScale = _isRunningAd ? 0 : origTimeScale;

                Application.runInBackground = _isRunningAd || origRunInBackground;
            }
        }

        public void beginAdBreakRewarded(AdBreakCompletedCallback completedCallback = null,
            AdErrorCallback errorCallback = null)
        {
            beginAdBreak(completedCallback, errorCallback, CrazyAdType.rewarded);
        }

        public void beginAdBreak(AdBreakCompletedCallback completedCallback = null, AdErrorCallback errorCallback = null,
            CrazyAdType adType = CrazyAdType.midgame)
        {
            CrazySDK.Instance.DebugLog("Requesting CrazyAd Type: " + adType);

            origTimeScale = Time.timeScale;
            origRunInBackground = Application.runInBackground;
            onCompletedAdBreak = completedCallback;
            onAdError = errorCallback;
            IsRunningAd = true;

#if UNITY_EDITOR
            SimulateAdBreak();
#else
        CrazySDK.Instance.RequestAd(adType);
#endif
        }

#if UNITY_EDITOR
        private IEnumerator InvokeRealtimeCoroutine(UnityAction action, float seconds)
        {
            yield return new WaitForSecondsRealtime(seconds);
            action();
        }

        private void SimulateAdBreak()
        {
            Debug.Log("CrazyAds: simulating ad request because we are in Editor .. game will resume in 3 seconds ..");
            var simulationPanel = new GameObject("Simulation Panel");

            simulationPanel.AddComponent<Canvas>();
            var canvas = simulationPanel.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 9999;

            simulationPanel.AddComponent<Image>();
            var background = simulationPanel.GetComponent<Image>();
            background.color = new Color32(46, 37, 68, 255);

            var panelText = new GameObject();
            panelText.AddComponent<Text>();
            ((RectTransform)panelText.transform).sizeDelta = new Vector2(500, 100);
            ((RectTransform)panelText.transform).position = new Vector2(Screen.width / 2, Screen.height / 2);
            var text = panelText.GetComponent<Text>();
            text.text = "Ad simulation the game will resume in 3 seconds";
            text.fontSize = 20;
            text.alignment = TextAnchor.MiddleCenter;
            text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");

            panelText.transform.parent = simulationPanel.transform;
            simulationPanel.transform.parent = transform;

            AdStarted();
            StartCoroutine(InvokeRealtimeCoroutine(EndSimulation, 3));
        }

        private void EndSimulation()
        {
            DestroyImmediate(GameObject.Find("Simulation Panel"));
            completedAdRequest();
        }
#endif

        private void completedAdRequest()
        {
            completedAdRequest(CrazySDKEvent.adCompleted);
        }

        private void completedAdRequest(CrazySDKEvent e)
        {
            CrazySDK.Instance.DebugLog("Ad Finished");

            IsRunningAd = false;

            if (e == CrazySDKEvent.adError && onAdError != null) onAdError.Invoke();
            else if (onCompletedAdBreak != null) onCompletedAdBreak.Invoke();
        }

        private void AdError(string error)
        {
            CrazySDK.Instance.DebugLog("Ad Error: " + error);
            completedAdRequest(CrazySDKEvent.adError);
        }

        private void AdFinished()
        {
            CrazySDK.Instance.DebugLog("Ad Finished");
            completedAdRequest(CrazySDKEvent.adFinished);
        }

        private void AdStarted()
        {
            CrazySDK.Instance.DebugLog("Ad Started");
            IsRunningAd = true;
        }

        #endregion

        #region Banners

        public void updateBannersDisplay()
        {
#if UNITY_EDITOR
            foreach (var banner in banners) banner.SimulateRender();
#else
        var visibleBanners = banners.Where(b => b.isVisible()).Select((crazyBanner) =>
        {
            var size = "";
            switch (crazyBanner.Size)
            {
                case CrazyBanner.BannerSize.Leaderboard_728x90:
                    size = "728x90";
                    break;
                case CrazyBanner.BannerSize.Medium_300x250:
                    size = "300x250";
                    break;
                case CrazyBanner.BannerSize.Mobile_320x50:
                    size = "320x50";
                    break;
                case CrazyBanner.BannerSize.Large_Mobile_320x100:
                    size = "320x100";
                    break;
                case CrazyBanner.BannerSize.Main_Banner_468x60:
                    size = "468x60";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            var bannerTranform = (RectTransform)crazyBanner.transform.Find("Banner");
            var anchor = new Vector2(
                (bannerTranform.anchorMin.x + bannerTranform.anchorMax.x) / 2,
                (bannerTranform.anchorMin.y + bannerTranform.anchorMax.y) / 2
            );
            return new Banner(
                crazyBanner.id,
                size,
                crazyBanner.Position,
                anchor
            );
        }).ToArray();
        CrazySDK.Instance.RequestBanners(visibleBanners);
#endif
        }

        public void registerBanner(CrazyBanner banner)
        {
            if (!banners.Contains(banner))
                banners.Add(banner);
        }

        public void unregisterBanner(CrazyBanner banner)
        {
            banners.Remove(banner);
        }

        public delegate void BannerRenderedCallback(string id);

        public void listenToBannerRendered(BannerRenderedCallback callback)
        {
            CrazySDK.Instance.AddEventListener(CrazySDKEvent.inGameBannerRendered, ev =>
            {
                var renderedEv = (BannerRenderedEvent)ev;
                callback(renderedEv.id);
            });
        }

        public delegate void BannerErrorCallback(string id, string error);

        public void listenToBannerError(BannerErrorCallback callback)
        {
            CrazySDK.Instance.AddEventListener(CrazySDKEvent.inGameBannerError, ev =>
            {
                var errorEv = (BannerErrorEvent)ev;
                callback(errorEv.id, errorEv.error);
            });
        }

        private void BannerError(string error)
        {
            CrazySDK.Instance.DebugLog("Banner error: " + error);
        }

        private void BannerRendered(string id)
        {
            CrazySDK.Instance.DebugLog("Banner with id " + id + " successfully rendered");
        }

        #endregion
    }
}