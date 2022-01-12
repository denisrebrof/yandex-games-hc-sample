using Levels.domain.repositories;
using Levels.presentation;
using UnityEngine;
using Zenject;

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
