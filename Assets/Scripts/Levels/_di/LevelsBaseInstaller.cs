using Balance.data;
using Balance.domain;
using Balance.domain.repositories;
using Levels.adapters;
using Levels.data;
using Levels.data.dao;
using Levels.domain;
using Levels.domain.repositories;
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
            Container.Bind<ILevelCompletedStateDao>().To<PlayerPrefsLevelCompletedStateDao>().AsSingle();
            Container.Bind<LevelsRepository.ILevelsDao>().FromInstance(levelsDao).AsSingle();
            Container.Bind<CurrentLevelRepository.ICurrentLevelIdDao>().To<PlayerPrefsCurrentLevelIdDao>().AsSingle();
            Container.Bind<CurrentLevelRepository.IDefaultLevelIdDao>().To<HardcodedDefaultLevelIdDao>().AsSingle();
            //Repositories
            Container.Bind<ILevelsRepository>().To<LevelsRepository>().AsCached();
            Container.Bind<ILevelSceneObjectRepository>().To<LevelsRepository>().AsCached();
            Container.Bind<ICurrentLevelRepository>().To<CurrentLevelRepository>().AsSingle();
            //UseCases
            Container.Bind<CompleteCurrentLevelUseCase>().ToSelf().AsSingle();
            Container.Bind<SetNextCurrentLevelUseCase>().ToSelf().AsSingle();
            //Adapters
            Container
                .Bind<CompleteCurrentLevelUseCase.IRewardHandler>()
                .To<DefaultLevelRewardHandlerAdapter>()
                .AsSingle();
        }
    }
}