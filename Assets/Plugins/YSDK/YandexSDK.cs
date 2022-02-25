using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Plugins.YSDK
{
    public class YandexSDK : MonoBehaviour
    {
        public static YandexSDK instance;

#if YANDEX_SDK
    [DllImport("__Internal")]
    private static extern void GetUserData();
    [DllImport("__Internal")]
    private static extern void RequestPlayerId();
    [DllImport("__Internal")]
    private static extern string GetLang();
    [DllImport("__Internal")]
    private static extern void ShowFullscreenAd();
    /// <summary>
    /// Returns an int value which is sent to index.html
    /// </summary>
    /// <param name="placement"></param>
    /// <returns></returns>
    [DllImport("__Internal")]
    private static extern int ShowRewardedAd(string placement);
    
    // [DllImport("__Internal")]
    // private static extern void GerReward();
    
    [DllImport("__Internal")]
    private static extern void AuthenticateUser();
    [DllImport("__Internal")]
    private static extern void InitPurchases();
    [DllImport("__Internal")]
    private static extern void Purchase(string id);
    [DllImport("__Internal")]
    private static extern void Hit(string id);
    
	[DllImport("__Internal")]
    private static extern string GetDeviceType();

    public UserData user;
    public event Action onUserDataReceived;

    public event Action<string> onPlayerIdReceived;
    public event Action onInterstitialShown;
    public event Action<string> onInterstitialFailed;
    /// <summary>
    /// Пользователь открыл рекламу
    /// </summary>
    public event Action<int> onRewardedAdOpened;
    /// <summary>
    /// Пользователь должен получить награду за просмотр рекламы
    /// </summary>
    public event Action<string> onRewardedAdReward;
    /// <summary>
    /// Пользователь закрыл рекламу
    /// </summary>
    public event Action<int> onRewardedAdClosed;
    /// <summary>
    /// Вызов/просмотр рекламы повлёк за собой ошибку
    /// </summary>
    public event Action<string> onRewardedAdError;
    /// <summary>


    public Queue<int> rewardedAdPlacementsAsInt = new();
    public Queue<string> rewardedAdsPlacements = new();

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Call this to show interstitial ad. Don't call frequently. There is a 3 minute delay after each show.
    /// </summary>
    public void ShowInterstitial() {
        ShowFullscreenAd();
    }

    public string GetLanguage()
    {
        return GetLang();
    }
    
    public void RequestPlayerIndentifier()
    {
        RequestPlayerId();
    }
	
	public bool GetIsOnDesktop() => !GetDeviceType().ToLower().Contains("mobile");

    /// <summary>
    /// Call this to show rewarded ad
    /// </summary>
    /// <param name="placement"></param>
    public void ShowRewarded(string placement) {
        rewardedAdPlacementsAsInt.Enqueue(ShowRewardedAd(placement));
        rewardedAdsPlacements.Enqueue(placement);
    }
    
    public void AddHit(string eventName) {
        Hit(eventName);
    }
    
    /// <summary>
    /// Call this to receive user data
    /// </summary>


    /// <summary>
    /// Callback from index.html
    /// </summary>
    public void OnInterstitialShown()
    {
        if (onInterstitialShown != null) onInterstitialShown.Invoke();
    }

    /// <summary>
    /// Callback from index.html
    /// </summary>
    /// <param name="error"></param>
    public void OnInterstitialFailed(string error)
    {
        if (onInterstitialFailed != null) onInterstitialFailed(error);
    }

    /// <summary>
    /// Callback from index.html
    /// </summary>
    /// <param name="placement"></param>
    public void OnRewardedOpen(int placement) {
        onRewardedAdOpened(placement);
    }

    /// <summary>
    /// Callback from index.html
    /// </summary>
    /// <param name="placement"></param>
    public void OnRewarded(int placement) {
        if (placement == rewardedAdPlacementsAsInt.Dequeue()) {
            onRewardedAdReward.Invoke(rewardedAdsPlacements.Dequeue());
        }
    }

    /// <summary>
    /// Callback from index.html
    /// </summary>
    /// <param name="placement"></param>
    public void OnRewardedClose(int placement) {
        onRewardedAdClosed(placement);
    }

    /// <summary>
    /// Callback from index.html
    /// </summary>
    /// <param name="placement"></param>
    public void OnRewardedError(string placement) {
        onRewardedAdError(placement);
        rewardedAdsPlacements.Clear();
        rewardedAdPlacementsAsInt.Clear();
    }
    
    /// <summary>
    /// Callback from index.html
    /// </summary>
    /// /// <param name="playerId"></param>
    public void OnHandlePlayerId(string playerId) {
        Debug.Log("Handle PId in YandexSDK");
        if(onPlayerIdReceived!=null) onPlayerIdReceived.Invoke(playerId);
    }

#endif
    }

    public struct UserData
    {
        public string id;
        public string name;
        public string avatarUrlSmall;
        public string avatarUrlMedium;
        public string avatarUrlLarge;
    }
}