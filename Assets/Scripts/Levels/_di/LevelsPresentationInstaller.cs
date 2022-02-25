using Levels.presentation;
using Levels.presentation.loader;
using Levels.presentation.ui;
using UnityEngine;
using Zenject;

namespace Levels._di
{
    public class LevelsPresentationInstaller : MonoInstaller
    {
        [SerializeField] private LevelSceneLoader loader;
        [SerializeField] private LevelLoadingController levelLoadingController;
        [SerializeField] private CompleteLevelController completeLevelController;
        [SerializeField] private LevelItem levelItemPrefab;

        public override void InstallBindings()
        {
            //Presentation
            Container.Bind<ILevelCompletedListener>().FromInstance(completeLevelController).AsSingle();
            Container.Bind<LevelSceneLoader.ILevelLoadingTransition>().To<EmptySceneLoadingTransition>().AsSingle();
            Container.Bind<LevelSceneLoader>().FromInstance(loader).AsSingle();
            Container.Bind<LevelLoadingController>().FromInstance(levelLoadingController).AsSingle();
            //UI
            Container.Bind<LevelItem.ILevelItemController>().To<DefaultLevelItemController>().AsSingle();
            Container.Decorate<LevelItem.ILevelItemController>().With<LevelItemControllerAnalyticsDecorator>();
            Container.BindFactory<LevelItem, LevelItem.Factory>().FromComponentInNewPrefab(levelItemPrefab);
        }
    }
}