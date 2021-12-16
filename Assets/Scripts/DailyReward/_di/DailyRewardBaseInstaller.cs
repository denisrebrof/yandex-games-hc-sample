using DailyReward.adapters;
using DailyReward.data;
using DailyReward.data.dao;
using DailyReward.domain;
using DailyReward.domain.repositories;
using UnityEngine;
using Zenject;

namespace DailyReward._di
{
    [CreateAssetMenu(fileName = "DailyRewardBaseInstaller", menuName = "Installers/DailyRewardBaseInstaller")]
    public class DailyRewardBaseInstaller: ScriptableObjectInstaller
    {
        [SerializeField] private DailyRewardDefaultDao rewardDefaultDao;
        
        public override void InstallBindings()
        {
            //Daos
            Container.Bind<PlayerPrefsDailyRewardRepository.IDailyRewardDao>().FromInstance(rewardDefaultDao).AsSingle();
            //Repositories
            Container.Bind<IDailyRewardRepository>().To<PlayerPrefsDailyRewardRepository>().AsSingle();
            //UseCases
            Container.Bind<DailyRewardCollectUseCase>().ToSelf().AsSingle();
            Container.Bind<DailyRewardStateUseCase>().ToSelf().AsSingle();
            Container.Bind<DailyRewardCooldownStateUseCase>().ToSelf().AsSingle();
            //Adapters
            Container
                .Bind<DailyRewardCollectUseCase.IDailyRewardCollectHandler>()
                .To<DailyRewardCollectHandlerAdapter>()
                .AsSingle();
        }
    }
}