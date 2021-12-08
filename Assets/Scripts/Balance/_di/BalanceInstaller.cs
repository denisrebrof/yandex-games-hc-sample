using Balance.data;
using Balance.domain;
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
            Container.Bind<DecreaseBalanceUseCase>().AsSingle();
        }
    }
}