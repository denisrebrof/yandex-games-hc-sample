using Zenject;

namespace Levels.presentation.ui
{
    public class DefaultLevelItemController : LevelItem.ILevelItemController
    {
        [Inject] private LevelLoadingController levelLoadingController;

        public void OnItemClick(long levelId)
        {
            levelLoadingController.LoadLevel(levelId);
        }
    }
}