using System.Collections.Generic;
using System.Linq;
using Levels.data.model;
using UnityEngine;

namespace Levels.data.dao
{
    [CreateAssetMenu(menuName = "Levels/LevelsDao/SimpleLevelsDao")]
    public class SimpleLevelsDao : ScriptableObject, LevelsRepository.ILevelsDao
    {
        [SerializeField] private int defaultReward = 100;

        [SerializeField] private List<GameObject> scenePrefabs = new();

        public List<LevelEntity> GetLevelEntities() => Enumerable
            .Range(0, scenePrefabs.Count)
            .Select(GetEntity)
            .ToList();

        private LevelEntity GetEntity(int levelId) => new LevelEntity(defaultReward, scenePrefabs[levelId]);
    }
}