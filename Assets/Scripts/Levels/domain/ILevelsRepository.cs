using System.Collections.Generic;
using Levels.domain.model;

namespace Levels.domain
{
    public interface ILevelsRepository
    {
        List<Level> GetLevels();
        Level GetLevel(long levelId);
        void SetLevelCompleted(long levelId);
    }
}