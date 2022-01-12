using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(menuName = "Installers/SDKInstaller")]
public class SDKInstaller : ScriptableObjectInstaller
{
    [SerializeField]
    private YandexSDK yandexSDK;
    [SerializeField]
    private VKSDK vksdk;
    [SerializeField]
    private PokiUnitySDK pokiUnitySDK;
    public override void InstallBindings()
    {
#if YANDEX_SDK
        var instance = Instantiate(yandexSDK);
        instance.gameObject.name = "YandexSDK";
        Container.Bind<YandexSDK>().FromInstance(instance).AsSingle();
#elif VK_SDK
        var instance = Instantiate(vksdk);
        instance.gameObject.name = "VKSDK";
        Container.Bind<VKSDK>().FromInstance(instance).AsSingle();
#elif POKI_SDK
        var instance = Instantiate(pokiUnitySDK);
        instance.gameObject.name = "POKI_SDK";
        Container.Bind<PokiUnitySDK>().FromInstance(instance).AsSingle();
#endif
    }
}
