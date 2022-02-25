using Levels.domain.repositories;
using Zenject;

namespace Levels.presentation.ui
{
    public class DefaultLevelItemController : LevelItem.ILevelItemController
    {
        [Inject] private LevelLoadingController levelLoadingController;
        [Inject] private ICurrentLevelRepository currentLevelRepository;

        public void OnItemClick(long levelId)
        {
            currentLevelRepository.SetCurrentLevel(levelId);
            levelLoadingController.LoadLevel(levelId);
        }
    }
}