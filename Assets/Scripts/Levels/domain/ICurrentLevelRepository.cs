using Levels.domain.model;

namespace Levels.domain
{
    public interface ICurrentLevelRepository
    {
        void SetCurrentLevel(long levelId);
        Level GetCurrentLevel();
        void SetCurrentLevelCompleted();
    }
}