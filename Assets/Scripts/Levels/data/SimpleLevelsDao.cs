using System.Collections.Generic;
using System.Linq;
using Levels.data.model;
using UnityEngine;

namespace Levels.data
{
    [CreateAssetMenu(menuName = "Levels/LevelsDao/SimpleLevelsDao")]
    public class SimpleLevelsDao : ScriptableObject, LevelsRepository.ILevelsDao
    {
        [SerializeField] private int defaultReward = 100;

        [SerializeField] private List<GameObject> scenePrefabs = new List<GameObject>();

        [SerializeField] private int levelsCount = 10;

        public List<LevelEntity> GetLevelEntities() => Enumerable.Range(0, levelsCount).Select(GetEntity).ToList();

        private LevelEntity GetEntity(int levelId) => new LevelEntity(defaultReward, scenePrefabs[levelId]);
    }
}