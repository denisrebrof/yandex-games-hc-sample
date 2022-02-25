using UnityEngine;

namespace Plugins.VKSDK
{
    public class VKSDK : MonoBehaviour
    {
        public static VKSDK instance;

#if VK_SDK

    public event Action onInterstitialShown;
    public event Action<string> onInterstitialFailed;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [DllImport("__Internal")]
    private static extern void ShowFullscreenAd();

    [DllImport("__Internal")]
    private static extern void ShowInviteBox();

    [DllImport("__Internal")]
    private static extern void ShowWallPostBox(string message, string attachments);

    /// <summary>
    /// Callback from index.html
    /// </summary>
    public void OnInterstitialShown()
    {
        Debug.Log("OnInterstitialShown callback");
        onInterstitialShown.Invoke();
    }

    /// <summary>
    /// Callback from index.html
    /// </summary>
    /// <param name="error"></param>
    public void OnInterstitialError(string error)
    {
        Debug.Log("OnInterstitialError callback");
        onInterstitialFailed.Invoke(error);
    }

    /// <summary>
    /// Call this to show interstitial ad. Don't call frequently. There is a 3 minute delay after each show.
    /// </summary>
    public void ShowInterstitial()
    {
        ShowFullscreenAd();
    }

    public void ShowInvite()
    {
        ShowInviteBox();
    }

    public void ShowWallPost(string message, string attachments)
    {
        ShowWallPostBox(message, attachments);
    }

#endif
    }
}