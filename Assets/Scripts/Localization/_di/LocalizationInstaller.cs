using System.ComponentModel;
using Localization.LanguageProviders;
using UnityEngine;
using Zenject;

namespace Localization._di
{
    [CreateAssetMenu(fileName = "LocalizationInstaller", menuName = "Installers/LocalizationInstaller")]
    public class LocalizationInstaller : ScriptableObjectInstaller<LocalizationInstaller>
    {
        public override void InstallBindings()
        {
            #if YANDEX_SDK
            Container.Bind<ILanguageProvider>().To<YandexLanguageProvider>().AsSingle();
            #elif VK_SDK
            Container.Bind<ILanguageProvider>().To<VKLanguageProvider>().AsSingle();
            #else
            Container.Bind<ILanguageProvider>().To<DefaultLanguageProvider>().AsSingle();
            #endif
        }
    }
}