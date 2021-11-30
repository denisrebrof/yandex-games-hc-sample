using Levels.domain;
using Levels.presentation.scene;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Levels.presentation
{
    public class LevelItemControllerDefault : MonoBehaviour, ILevelItemController
    {
        [SerializeField] private UnityAction onLoadLevel;
        [Inject] private ILevelSceneObjectRepository repository;
        [Inject] private LevelSceneLoader loader;

        public void OnItemClick(long levelId)
        {
            var scene = repository.GetLevelScene(levelId);
            loader.LoadLevel(scene);
            onLoadLevel.Invoke();
        }
    }
}