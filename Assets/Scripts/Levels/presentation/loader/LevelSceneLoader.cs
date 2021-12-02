using System;
using UnityEngine;
using Zenject;

namespace Levels.presentation.loader
{
    public class LevelSceneLoader : MonoBehaviour
    {
        [SerializeField] private Transform levelHolder;
        [Inject] private ILevelLoadingTransition levelSwitchAnimator;
        private GameObject levelToLoad;
        private bool isLevelSwitching;

        private void Start()
        {
            if (levelHolder != null) return;
            var foundedLevelHolder = GameObject.FindWithTag("LevelHolder");
            if (foundedLevelHolder != null)
                levelHolder = foundedLevelHolder.transform;
        }

        public void LoadLevel(GameObject level)
        {
            levelToLoad = level;
            if (isLevelSwitching)
                return;

            isLevelSwitching = true;
            levelSwitchAnimator.StartAnimation(
                SpawnLevel,
                TryLoadLevel
            );
        }

        private void TryLoadLevel()
        {
            isLevelSwitching = false;
            if (levelToLoad != null)
                LoadLevel(levelToLoad);
        }

        private void SpawnLevel()
        {
            while (levelHolder.childCount > 0)
                DestroyImmediate(levelHolder.GetChild(0).gameObject);
            Instantiate(levelToLoad, levelHolder.position, levelHolder.rotation, levelHolder);
            levelToLoad = null;
        }
        
        public interface ILevelLoadingTransition
        {
            void StartAnimation(Action onSceneHidden = null, Action onCompleted = null);
        }
    }
}