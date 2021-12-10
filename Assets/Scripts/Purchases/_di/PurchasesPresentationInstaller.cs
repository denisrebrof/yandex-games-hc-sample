using Levels.presentation;
using Levels.presentation.loader;
using Levels.presentation.ui;
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
            Container.BindFactory<Purchas, LevelItem.Factory>().FromComponentInNewPrefab(levelItemPrefab);
        }
    }
}