using Balance.data;
using Balance.domain;
using Balance.domain.repositories;
using UnityEngine;
using Zenject;

namespace Balance._di
{
    [CreateAssetMenu(menuName ="Installers/BalanceInstaller")]
    public class BalanceInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IBalanceRepository>().To<PlayerPrefsBalanceRepository>().AsSingle();
            Container.Bind<IRewardRepository>().To<RewardInMemoryRepository>().AsSingle();
            Container.Bind<DecreaseBalanceUseCase>().AsSingle();
            Container.Bind<CollectRewardUseCase>().AsSingle();
        }
    }
}