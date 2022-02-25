#if YANDEX_SDK && !UNITY_EDITOR
using System;
using Plugins.YSDK;
using SDK.PlayerData.domain;
using UniRx;
using UnityEngine;
using Zenject;

namespace SDK.PlayerData.data
{
    public class YandexPlayerIdRepository : IPlayerIdRepository 
    {
        [Inject] private YandexSDK instance;

        public bool GetPlayerIdAvailable() => true;
        public IObservable<string> InitializeWithPlayerId()
        {
            return Observable.FromEvent<string>(
                handler => instance.onPlayerIdReceived += handler,
                handler => instance.onPlayerIdReceived -= handler
            ).DoOnSubscribe(() => instance.RequestPlayerIndentifier());
        }
    }
}
#endif