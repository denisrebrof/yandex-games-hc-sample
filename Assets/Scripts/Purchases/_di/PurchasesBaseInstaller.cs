using Purchases.adapters;
using Purchases.data;
using Purchases.data.dao;
using Purchases.domain;
using Purchases.domain.repositories;
using UnityEngine;
using Zenject;

namespace Purchases._di
{
    [CreateAssetMenu(fileName = "PurchasesBaseInstaller", menuName = "Installers/PurchasesBaseInstaller")]
    public class PurchasesBaseInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private SimplePurchaseEntitiesDao purchaseEntitiesDao;
        public override void InstallBindings()
        {
            //Daos
            Container.Bind<ISavedPurchasedStateDao>().To<PlayerPrefsPurchasedStateDao>().AsSingle();
            Container
                .Bind<RewardedVideoPurchaseRepository.IRewardedVideoWatchDao>()
                .To<PlayerPrefsRewardedVideoWatchesDao>()
                .AsSingle();
            Container.Bind<IPurchaseEntitiesDao>().FromInstance(purchaseEntitiesDao).AsSingle();
            //Repositories
            Container.Bind<IPurchaseRepository>().To<PurchaseRepository>().AsSingle();
            Container.Bind<ICoinsPurchaseRepository>().To<CoinsPurchaseRepository>().AsSingle();
            Container.Bind<IPassLevelRewardPurchasesRepository>().To<PassLevelRewardPurchasesRepository>().AsSingle();
            Container.Bind<IRewardedVideoPurchaseRepository>().To<RewardedVideoPurchaseRepository>().AsSingle();
            //UseCases
            Container.Bind<ExecutePurchaseUseCase>().ToSelf().AsSingle();
            Container.Bind<PurchasedStateUseCase>().ToSelf().AsSingle();
            //Adapters
            Container
                .Bind<ExecutePurchaseUseCase.IBalanceAccessProvider>()
                .To<BalanceAccessProviderAdapter>()
                .AsSingle();
            Container
                .Bind<PurchasedStateUseCase.ILevelPassedStateProvider>()
                .To<LevelPassedStateProviderAdapter>()
                .AsSingle();
        }
    }
}