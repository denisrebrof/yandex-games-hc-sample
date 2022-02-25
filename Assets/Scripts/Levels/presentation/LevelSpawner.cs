using Levels.domain.repositories;
using UnityEngine;
using Zenject;

namespace Levels.presentation
{
    public class LevelSpawner : MonoBehaviour
    {
        [Inject] private ICurrentLevelRepository currentLevelRepository;
        [Inject] private LevelLoadingController levelLoadingController;

        private void Awake()
        {
            var currentLevel = currentLevelRepository.GetCurrentLevel();
            levelLoadingController.LoadLevel(currentLevel.ID);
        }
    }
}
