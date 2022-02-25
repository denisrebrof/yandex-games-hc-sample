using System;

namespace Levels.domain.repositories
{
    public interface ILevelCompletedStateRepository
    {
        IObservable<bool> GetLevelCompletedState(long levelId);
        bool GetLevelCompletedStateValue(long levelId);
        void SetLevelCompleted(long levelId);
    }
}