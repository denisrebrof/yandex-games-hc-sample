using System.Collections.Generic;
using Levels.domain.model;

namespace Levels.domain.repositories
{
    public interface ILevelsRepository
    {
        List<Level> GetLevels();
        Level GetLevel(long levelId);
    }
}