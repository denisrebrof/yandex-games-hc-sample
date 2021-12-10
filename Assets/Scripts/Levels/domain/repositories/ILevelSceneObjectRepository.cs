using UnityEngine;

namespace Levels.domain.repositories
{
    public interface ILevelSceneObjectRepository
    {
        GameObject GetLevelScene(long levelId);
    }
}