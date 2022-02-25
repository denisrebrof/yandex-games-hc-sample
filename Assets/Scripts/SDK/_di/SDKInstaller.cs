using System.Linq;
using CrazyGames;
using Plugins.VKSDK;
using SDK.GameState;
using SDK.HappyTime;
using SDK.HappyTime.controller;
using SDK.Platform.domain;
using UnityEngine;
using Zenject;

namespace SDK._di
{
    [CreateAssetMenu(menuName = "Installers/SDKInstaller")]
    public class SDKInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private Plugins.YSDK.YandexSDK yandexSDK;
        [SerializeField] private VKSDK vksdk;
        [SerializeField] private PokiUnitySDK pokiUnitySDK;

        [SerializeField] private CrazySDK crazySDK;

        public override void InstallBindings()
        {
            InstallGameStateNavigator();
            InstallSDK();
            InstallHappyTime();
            InstallPlatformProvider();
        }

        private void InstallHappyTime()
        {
            Container
                .Bind<IHappyTimeController>()
#if POKI_SDK
                .To<PokiHappyTimeController>()
#elif CRAZY_SDK
                .To<CrazyHappyTimeController>()
#else
                .To<DebugLogHappyTimeController>()
#endif
                .AsSingle();
        }
    
        private void InstallGameStateNavigator()
        {
            var gameStateNavigator = FindObjectsOfType<MonoBehaviour>().OfType<IGameStateNavigator>().First(); 
            Container.Bind<IGameStateNavigator>().FromInstance(gameStateNavigator).AsSingle();
        }

        private void InstallSDK()
        {
#if YANDEX_SDK
            var instance = Instantiate(yandexSDK);
            instance.gameObject.name = "YandexSDK";
            Container.Bind<Plugins.YSDK.YandexSDK>().FromInstance(instance).AsSingle();
#elif VK_SDK
        var instance = Instantiate(vksdk);
        instance.gameObject.name = "VKSDK";
        Container.Bind<VKSDK>().FromInstance(instance).AsSingle();
#elif POKI_SDK
        var instance = Instantiate(pokiUnitySDK);
        instance.gameObject.name = "POKI_SDK";
        Container.Bind<PokiUnitySDK>().FromInstance(instance).AsSingle();
#elif CRAZY_SDK
        var instance = Instantiate(crazySDK);
        instance.gameObject.name = "CrazySDK";
        Container.Bind<CrazySDK>().FromInstance(instance).AsSingle();
#endif
        }

        private void InstallPlatformProvider()
        {
#if YANDEX_SDK && !UNITY_EDITOR
        Container.Bind<IPlatformProvider>().To<YandexPlatformProvider>().AsSingle();
// #elif UNITY_EDITOR
//         Container.Bind<IPlatformProvider>().To<MobilePlatformProvider>().AsSingle();
#else
            Container.Bind<IPlatformProvider>().To<DesktopPlatformProvider>().AsSingle();
#endif
        }
    }
}