using Levels.domain;
using Levels.presentation.loader;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Levels.presentation
{
    public class LevelLoadingController : MonoBehaviour
    {
        public UnityEvent onLoadLevel;
        [Inject] private ILevelSceneObjectRepository repository;
        [Inject] private LevelSceneLoader loader;

        public void LoadLevel(long levelId)
        {
            Debug.Log("Load level " + levelId);
            var scene = repository.GetLevelScene(levelId);
            loader.LoadLevel(scene);
            onLoadLevel.Invoke();
        }
    }
}