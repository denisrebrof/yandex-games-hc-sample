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
        [SerializeField] private AnimatorLevelLoaderTransition transition;
        [SerializeField] private LevelItem levelItemPrefab;

        public override void InstallBindings()
        {
            //Presentation
            Container.Bind<ILevelCompletionHandler>().FromInstance(completeLevelController).AsSingle();
            // Container.Bind<LevelSceneLoader.ILevelLoadingTransition>().FromInstance(transition).AsSingle();
            Container.Bind<LevelSceneLoader.ILevelLoadingTransition>().To<EmptySceneLoadingTransition>().AsSingle();
            Container.Bind<LevelSceneLoader>().FromInstance(loader).AsSingle();
            Container.Bind<LevelLoadingController>().FromInstance(levelLoadingController).AsSingle();
            //UI
            Container.Bind<LevelItem.ILevelItemController>().To<DefaultLevelItemController>().AsSingle();
            Container.BindFactory<LevelItem, LevelItem.Factory>().FromComponentInNewPrefab(levelItemPrefab);
        }
    }
}