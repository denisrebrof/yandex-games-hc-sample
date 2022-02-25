using SDK.PlayerData.data;
using SDK.PlayerData.domain;
using UnityEngine;
using Zenject;

namespace SDK.PlayerData._di
{
    [CreateAssetMenu(menuName = "Installers/PlayerDataInstaller")]
    public class PlayerDataInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IPlayerIdRepository>()
#if YANDEX_SDK && !UNITY_EDITOR
                .To<YandexPlayerIdRepository>()
#elif CRAZY_SDK
                .To<AnonimousPlayerIdRepository>()
                #else
                .To<EmptyPlayerIdRepository>()
#endif
                .AsSingle();
        }
    }
}