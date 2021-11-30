using UnityEngine;

namespace Levels.domain
{
    public interface ILevelSceneObjectRepository
    {
        GameObject GetLevelScene(long levelId);
    }
}