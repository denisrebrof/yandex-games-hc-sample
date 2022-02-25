using Analytics.session.domain;
using Levels.adapters;
using Levels.data;
using Levels.data.dao;
using Levels.domain;
using Levels.domain.repositories;
using Levels.presentation.analytics;
using UnityEngine;
using Zenject;

namespace Levels._di
{
    [CreateAssetMenu(menuName = "Installers/LevelsBaseInstaller")]
    public class LevelsBaseInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private SimpleLevelsDao levelsDao;

        public override void InstallBindings()
        {
            //Daos
            Container.Bind<ILevelCompletedStateDao>()
#if PLAYER_PREFS_STORAGE
                .To<PlayerPrefsLevelCompletedStateDao>()
#else
                .To<LocalStorageLevelCompletedStateDao>()
#endif
                .AsSingle();


            Container.Bind<LevelsRepository.ILevelsDao>().FromInstance(levelsDao).AsSingle();

            Container.Bind<CurrentLevelRepository.ICurrentLevelIdDao>()
#if PLAYER_PREFS_STORAGE
                .To<PlayerPrefsCurrentLevelIdDao>()
#else
                .To<LocalStorageCurrentLevelIdDao>()
#endif
                .AsSingle();


            Container.Bind<CurrentLevelRepository.IDefaultLevelIdDao>().To<HardcodedDefaultLevelIdDao>().AsSingle();
            //Repositories
            Container.Bind<ILevelsRepository>().To<LevelsRepository>().AsCached();
            Container.Bind<ILevelSceneObjectRepository>().To<LevelsRepository>().AsCached();
            Container.Bind<ICurrentLevelRepository>().To<CurrentLevelRepository>().AsSingle();
            Container.Bind<ILevelCompletedStateRepository>().To<LevelCompletedStateRepository>().AsSingle();
            //UseCases
            Container.Bind<CompleteCurrentLevelUseCase>().ToSelf().AsSingle();
            Container.Bind<SetNextCurrentLevelUseCase>().ToSelf().AsSingle();
            //Adapters
            Container
                .Bind<CompleteCurrentLevelUseCase.IRewardHandler>()
                .To<StubLevelRewardHandlerAdapter>()
                .AsSingle();
            Container
                .Bind<ISessionEventLevelIdProvider>()
                .To<SessionEventCurrentLevelIdProviderImpl>()
                .AsSingle();
        }
    }
}