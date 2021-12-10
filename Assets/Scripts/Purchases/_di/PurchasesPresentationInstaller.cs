using Levels.presentation;
using Levels.presentation.loader;
using Levels.presentation.ui;
using Purchases.presentation.ui;
using UnityEngine;
using Zenject;

namespace Purchases._di
{
    public class PurchasesPresentationInstaller : MonoInstaller
    {

        public override void InstallBindings()
        {
            //Presentation
            //UI
            Container.Bind<LevelItem.ILevelItemController>().To<DefaultLevelItemController>().AsSingle();
            //Item Factories
            Container.BindFactory<CoinsPurchaseItem, CoinsPurchaseItem.Factory>().AsCached();
            Container.BindFactory<PassLevelRewardItem, PassLevelRewardItem.Factory>().AsCached();
            // Container.BindFactory<CoinsPurchaseItem, CoinsPurchaseItem.Factory>().AsCached();
        }
    }
}