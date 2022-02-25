using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using UnityEngine;

namespace CrazyGames
{
    
    public enum CrazyAdType
    {
        midgame,
        rewarded
    }

    public enum CrazySDKEvent
    {
        adStarted, // fired when ad starts playing
        adFinished, // fired when ad has finished (either when completed or when user pressed skip)
        adCompleted, // fired when user has completely watched the ad
        adError, // fired when an error occurs, also fired when no ad is available
        adblockDetectionExecuted, // fired when adblock detection has run
        inGameBannerError, // fired when an error happened when rendering in-game banners
        inGameBannerRendered, // fired when a banner has been rendered
        requestScreenshot,
    }

    public class CrazySDK : Singleton<CrazySDK>
    {
        public static string sdkVersion = "3.1.1";
        public bool debug;
        private bool adblockDetectionExecuted;

        private Dictionary<CrazySDKEvent, List<EventCallback>> eventListeners;
        private bool hasAdblock;
        private InitializationObject initObj;
        private bool requestInProgress;
        private bool noGameframe;

#if UNITY_WEBGL
        [DllImport("__Internal")]
        private static extern bool InitSDK(string version, string objectName);

        [DllImport("__Internal")]
        private static extern void GetScreenshotSDK(byte[] img, int size);

        [DllImport("__Internal")]
        private static extern void RequestAdSDK(string str);

        [DllImport("__Internal")]
        private static extern void HappyTimeSDK();

        [DllImport("__Internal")]
        private static extern void GameplayStartSDK();

        [DllImport("__Internal")]
        private static extern void GameplayStopSDK();

        [DllImport("__Internal")]
        private static extern void RequestInviteUrlSDK(string url);

        [DllImport("__Internal")]
        private static extern void CopyToClipboardSDK(string text);

        [DllImport("__Internal")]
        private static extern string GetUrlParametersSDK();

        [DllImport("__Internal")]
        private static extern string RequestBannersSDK(string bannerRequestJSON);
#else
        // Preventing build to fail when using another platform than WebGL
        private bool InitSDK(string version, string objectName) { return false; }
        private void GetScreenshotSDK(byte[] img, int size) {}
        private void RequestAdSDK(string str) { }
        private void HappyTimeSDK() { }
        private void GameplayStartSDK() { }
        private void GameplayStopSDK() { }
        private void RequestInviteUrlSDK(string url) { }
        private void CopyToClipboardSDK(string text) { }
        private string GetUrlParametersSDK() { return ""; }
        private string RequestBannersSDK(string bannerRequestJSON) { return ""; }
#endif

        private void Awake()
        {
            if (Debug.isDebugBuild)
                // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                Debug.LogWarning("[CrazySDK] Development Build");

            eventListeners = new Dictionary<CrazySDKEvent, List<EventCallback>>();
        }

        private void Start()
        {
#if (!UNITY_EDITOR)
            debug = GetUrlParameter("sdk_debug") == "true";
            noGameframe = !InitSDK(sdkVersion, "CrazyGames.CrazySDK");
            if (noGameframe)
            {
                Debug.Log("No Crazygames gameframe detected, cannot run SDK.");
                DebugLog("No Crazygames gameframe detected, cannot run SDK.");
            }
#else
            debug = true;
            initObj = new InitializationObject { gameLink = "https://www.crazygames.com/game/yourFabulousGameHere" };
            DebugLog("Running into Unity Editor, Crazygames SDK is emulated");
#endif
        }

        #region init

        // Gameframe init callback
        public void InitCallback(string initObjJSON)
        {
            DebugLog("Init callback from gameframe");
            DebugLog("Init obj: " + initObjJSON);
            initObj = JsonUtility.FromJson<InitializationObject>(initObjJSON);
            AddEventListener(CrazySDKEvent.requestScreenshot, RequestScreenshotListener);
        }

        private void RequestScreenshotListener(SDKGFEvent ev)
        {
            StartCoroutine(GetScreenshotRoutine());
        }

        #endregion

        public IEnumerator GetScreenshotRoutine()
        {
            yield return new WaitForEndOfFrame();
#if (!UNITY_EDITOR)
            var tex = new Texture2D(Screen.width, Screen.height);
            tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            tex.Apply();
            var img = tex.EncodeToPNG();
            wrapGFFeature(() => { GetScreenshotSDK(img, img.Length); });
#else
            Debug.Log("GetScreenshotRoutine");
#endif
        }

        #region callableActions

        public void RequestAd(CrazyAdType adType = CrazyAdType.midgame)
        {
            DebugLog("Requesting " + adType + " ad");

            if (requestInProgress)
            {
                DebugLog("Request in progress");
                return;
            }

            requestInProgress = true;
#if (!UNITY_EDITOR)
            wrapGFFeature(() => { RequestAdSDK(adType.ToString()); });
#else
            AdEvent("adError");
#endif
        }

        public void HappyTime()
        {
            DebugLog("Happy time!");

#if (!UNITY_EDITOR)
            wrapGFFeature(HappyTimeSDK);
#else
            Debug.Log("Happytime simulation yeah! Here have a cookie :D");
#endif
        }

        public void GameplayStart()
        {
            DebugLog("Gameplay start called");

#if (!UNITY_EDITOR)
		    wrapGFFeature(GameplayStartSDK);
#else
            Debug.Log("Gameplay start simulation for unity editor");
#endif
        }

        public void GameplayStop()
        {
            DebugLog("Gameplay stop called");

#if (!UNITY_EDITOR)
		    wrapGFFeature(GameplayStopSDK);
#else
            Debug.Log("Gameplay stop simulation for unity editor");
#endif
        }

        public string InviteLink(Dictionary<string, string> parameters)
        {
            DebugLog("Invite link called");

            var queryFromParams = parameters
                .Select(parameter => string.Format("{0}={1}", parameter.Key, parameter.Value)).Aggregate(
                    "utm_source=invite", (current, paramString) => string.Format("{0}&{1}", current, paramString));

            if (initObj == null)
            {
                DebugLog("Initialization object not received from gameframe, cannot generate invite link");
                return null;
            }

            var gameLink = initObj.gameLink;
            var template = gameLink.Contains("?") ? "{0}&{1}" : "{0}?{1}";
            var inviteLink = string.Format(template, gameLink, queryFromParams);

#if (!UNITY_EDITOR)
            wrapGFFeature(() => { RequestInviteUrlSDK(inviteLink); });
#else
            Debug.Log("Invite link simulation for unity editor");
#endif

            DebugLog("Invite link is " + inviteLink);
            return inviteLink;
        }

        public void CopyToClipboard(string text)
        {
#if (!UNITY_EDITOR)
		    CopyToClipboardSDK(text);
#endif
        }

        public bool IsInviteLink()
        {
#if (!UNITY_EDITOR)
            var utmSource = GetUrlParameter("utm_source");
            return utmSource == "invite";
#else
            Debug.Log("Cannot parse url in Unity editor, try running it in a browser");
            return false;
#endif
        }

        public string GetInviteLinkParameter(string key)
        {
#if (!UNITY_EDITOR)
            return GetUrlParameter(key);
#else
            Debug.Log("Cannot parse url in Unity editor, try running it in a browser");
            return "";
#endif
        }

        public void RequestBanners(Banner[] banners)
        {
            DebugLog("Requesting banners to gameframe" + CrazyJsonHelper.ToJson(banners));
#if (!UNITY_EDITOR)
            wrapGFFeature(() => { RequestBannersSDK(CrazyJsonHelper.ToJson(banners)); });
#else
            Debug.Log("Cannot render banners inside Unity editor");
#endif
        }

        #endregion


        #region eventManagement

        public delegate void EventCallback(SDKGFEvent ev);

        public void AddEventListener(CrazySDKEvent eventType, EventCallback callback)
        {
            DebugLog("Adding event listener for " + eventType);

            if (!eventListeners.ContainsKey(eventType)) eventListeners.Add(eventType, new List<EventCallback>());

            eventListeners[eventType].Add(callback);
        }

        public void RemoveEventListener(CrazySDKEvent eventType, EventCallback callback)
        {
            DebugLog("Removing event listener for " + eventType);

            if (eventListeners.ContainsKey(eventType)) eventListeners[eventType].Remove(callback);
        }

        public void RemoveEventListenersForEvent(CrazySDKEvent eventType)
        {
            DebugLog("Removing all event listener for " + eventType);

            eventListeners.Remove(eventType);
        }

        public void RemoveAllEventListeners()
        {
            DebugLog("Removing all event listeners");

            eventListeners.Clear();
        }

        // Gameframe events callbacks
        public void AdEvent(string eventJSON)
        {
            DebugLog("Received event from gameframe");
            DebugLog(eventJSON);
            var receivedEvent = JsonUtility.FromJson<GFEvent>(eventJSON);

            var parsedEventType =
                (CrazySDKEvent)Enum.Parse(typeof(CrazySDKEvent), receivedEvent.name);
            SDKGFEvent evt;
            switch (parsedEventType)
            {
                case CrazySDKEvent.inGameBannerError:
                    var errorEv = JsonUtility.FromJson<GFBannerErrorEvent>(eventJSON);
                    evt = new BannerErrorEvent(errorEv.id, errorEv.error);
                    break;
                case CrazySDKEvent.inGameBannerRendered:
                    evt = new BannerRenderedEvent(JsonUtility.FromJson<GFBannerRenderedEvent>(eventJSON).id);
                    break;
                case CrazySDKEvent.adError:
                    evt = new AdErrorEvent(JsonUtility.FromJson<GFAdErrorEvent>(eventJSON).error);
                    break;
                default:
                    evt = new SDKGFEvent(parsedEventType);
                    break;
            }

            HandleEvent(evt);
            CallCallbacks(evt);
        }

        private void HandleEvent(SDKGFEvent ev)
        {
            switch (ev.type)
            {
                case CrazySDKEvent.adFinished:
                case CrazySDKEvent.adError:
                    requestInProgress = false;
                    break;
            }
        }

        private void CallCallbacks(SDKGFEvent ev)
        {
            DebugLog("Calling callbacks for event " + ev.type);
            if (!eventListeners.ContainsKey(ev.type)) return;
            foreach (var callback in eventListeners[ev.type]) callback(ev);
        }

        #endregion


        #region adblock

        public bool HasAdblock()
        {
            if (!adblockDetectionExecuted) DebugLog("Adblock detection has not finished");

            return hasAdblock;
        }

        public bool AdblockDetectionExecuted()
        {
            return adblockDetectionExecuted;
        }

        // Gameframe adblock is detected callback
        public void AdblockDetected()
        {
            Adblock(true);
        }

        // Gameframe adblock is not detected callback
        public void AdblockNotDetected()
        {
            Adblock(false);
        }

        private void Adblock(bool detected)
        {
            DebugLog("Adblock detection executed");
            DebugLog("Adblock present: " + detected);
            adblockDetectionExecuted = true;
            hasAdblock = detected;
            CallCallbacks(new SDKGFEvent(CrazySDKEvent.adblockDetectionExecuted));
        }

        #endregion


        #region utils

        private string GetUrlParameter(string key)
        {
            var _regex = new Regex(@"[?&](\w[\w.]*)=([^?&]+)");
            var paramsStr = GetUrlParametersSDK();
            var match = _regex.Match(paramsStr);
            var parameters = new Dictionary<string, string>();
            while (match.Success)
            {
                parameters.Add(match.Groups[1].Value, match.Groups[2].Value);
                match = match.NextMatch();
            }

            try
            {
                return parameters[key];
            }
            catch
            {
                return null;
            }
        }

        private void wrapGFFeature(Action action)
        {
            if (noGameframe)
            {
                DebugLog("Cannot call Crazygames SDK features as gameframe hasn't be detected.");
                return;
            }

            action();
        }

        public void DebugLog(string msg)
        {
            if (Debug.isDebugBuild || debug) Debug.Log("[CrazySDK] " + msg);
        }

        #endregion
    }

    [Serializable]
    public class Banner
    {
        public string id;
        public string size;
        public Vector2 position;
        public Vector2 anchor;

        public Banner(string id, string size, Vector2 position, Vector2 anchor)
        {
            this.id = id;
            this.size = size;
            this.position = position;
            this.anchor = anchor;
        }
    }

    [Serializable]
    public class GFEvent
    {
        public string name;
    }

    [Serializable]
    public class GFAdErrorEvent : GFEvent
    {
        public string error;
    }

    [Serializable]
    public class GFBannerErrorEvent : GFEvent
    {
        public string id;
        public string error;
    }

    [Serializable]
    public class GFBannerRenderedEvent : GFEvent
    {
        public string id;
    }

    public class SDKGFEvent
    {
        public CrazySDKEvent type;

        public SDKGFEvent(CrazySDKEvent type)
        {
            this.type = type;
        }
    }

    public class AdErrorEvent : SDKGFEvent
    {
        public string error;

        public AdErrorEvent(string error) : base(CrazySDKEvent.adError)
        {
            this.error = error;
        }
    }

    public class BannerErrorEvent : SDKGFEvent
    {
        public readonly string error;
        public readonly string id;

        public BannerErrorEvent(string id, string error) : base(CrazySDKEvent.inGameBannerError)
        {
            this.id = id;
            this.error = error;
        }
    }

    public class BannerRenderedEvent : SDKGFEvent
    {
        public string id;

        public BannerRenderedEvent(string id) : base(CrazySDKEvent.inGameBannerRendered)
        {
            this.id = id;
        }
    }

    public class InitializationObject
    {
        public string gameLink;
    }
}