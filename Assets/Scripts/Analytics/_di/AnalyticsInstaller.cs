using Analytics.adapter;
using Analytics.session.data;
using Analytics.session.domain;
using UnityEngine;
using Zenject;
#if GAME_ANALYTICS
using GameAnalyticsSDK;
#endif

namespace Analytics._di
{
    [CreateAssetMenu(menuName = "Installers/AnalyticsInstaller")]
    public class AnalyticsInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings() => BindAnalyticsAdapter();

        private void BindAnalyticsAdapter()
        {
            Container
                .Bind<AnalyticsAdapter>()
#if GAME_ANALYTICS
                .To<GameAnalyticsAdapter>()
#elif DEBUG_ANALYTICS
                .FromInstance(new DebugLogAnalyticsAdapter(true))
#else
                .FromInstance(new DebugLogAnalyticsAdapter(false))
#endif
                .AsSingle();

            Container
                .Bind<IFirstOpenEventSentRepository>()
#if PLAYER_PREFS_STORAGE
                .To<PlayerPrefsFirstOpenEventSentRepository>()
#else
                .To<LocalStorageFirstOpenEventSentRepository>()
#endif
                .AsSingle();
        }
    }
}