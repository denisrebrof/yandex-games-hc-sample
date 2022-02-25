using Doozy.Engine;
using UniRx;
using UnityEngine;

namespace Ads.presentation.InterstitialAdNavigator
{
    public class ShowInterstitialBridge: MonoBehaviour
    {
        private IInterstitalAdNavigator navigator;
        [SerializeField] private string onShownEvent;
        [SerializeField] private string onFailedEvent;

        private void Awake()
        {
            navigator = GetComponent<IInterstitalAdNavigator>();
        }

        public void TryShow()
        {
            navigator.ShowAd().Subscribe(
                res =>
                {
                    var message = res.isSuccess ? onShownEvent : onFailedEvent;
                    GameEventMessage.SendEvent(message);   
                },
                e => GameEventMessage.SendEvent(onFailedEvent)
            ).AddTo(this);
        }
    }
}