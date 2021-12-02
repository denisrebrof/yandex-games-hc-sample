using Levels.domain;
using Levels.presentation.loader;
using Levels.presentation.ui;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Levels.presentation
{
    public class LevelLoadingController : MonoBehaviour
    {
        [SerializeField] private UnityAction onLoadLevel;
        [Inject] private ILevelSceneObjectRepository repository;
        [Inject] private LevelSceneLoader loader;

        public void LoadLevel(long levelId)
        {
            var scene = repository.GetLevelScene(levelId);
            loader.LoadLevel(scene);
            onLoadLevel.Invoke();
        }
    }
}