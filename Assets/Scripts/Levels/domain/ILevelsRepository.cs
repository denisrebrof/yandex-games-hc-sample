using System.Collections.Generic;
using Levels.domain.model;

namespace Levels.domain
{
    public interface ILevelsRepository
    {
        List<Level> GetLevels();
        void CompleteLevel(long levelId);
    }
}