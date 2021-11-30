using System;
using System.Collections.Generic;
using System.Linq;
using Levels.data.model;
using Levels.domain;
using Levels.domain.model;
using UnityEngine;
using Zenject;

namespace Levels.data
{
    public class LevelsRepository : ILevelsRepository, ILevelSceneObjectRepository
    {
        [Inject] private ILevelsDao levelsDao;
        [Inject] private ILevelCompletedStateDao completedStateDao;

        public List<Level> GetLevels() => levelsDao.GetLevelEntities().Select(GetLevel).ToList();

        private Level GetLevel(LevelEntity entity, int number)
        {
            var id = Convert.ToInt64(number);
            var completed = completedStateDao.IsCompleted(id);
            return new Level(id, number, completed, entity.reward);
        }

        public void CompleteLevel(long levelId) => completedStateDao.SetCompleted(levelId);

        public GameObject GetLevelScene(long levelId)
        {
            var index = Convert.ToInt32(levelId);
            return levelsDao.GetLevelEntities()[index].scenePrefab;
        }
        
        public interface ILevelsDao
        {
            public List<LevelEntity> GetLevelEntities();
        }
    }
}