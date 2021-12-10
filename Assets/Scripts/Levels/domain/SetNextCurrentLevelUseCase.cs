using System.Linq;
using Levels.domain.model;
using Levels.domain.repositories;
using Zenject;

namespace Levels.domain
{
    public class SetNextCurrentLevelUseCase
    {
        [Inject] private ILevelsRepository levelsRepository;
        [Inject] private ICurrentLevelRepository currentLevelRepository;

        public void SetNextCurrentLevel()
        {
            var currentLevel = currentLevelRepository.GetCurrentLevel();
            var nextLevel = GetNextLevel(currentLevel);
            currentLevelRepository.SetCurrentLevel(nextLevel.ID);
        }

        private Level GetNextLevel(Level currentLevel)
        {
            var nextLevels = levelsRepository
                .GetLevels()
                .Where(level => level.Number > currentLevel.Number)
                .OrderBy(level => level.Number)
                .ToList();

            if (nextLevels.Count == 0)
                return currentLevel;

            if (nextLevels.All(level => level.CompletedState))
                return nextLevels.First();

            return nextLevels.First(level => !level.CompletedState);
        }
    }
}