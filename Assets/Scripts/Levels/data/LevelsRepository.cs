using System;
using System.Collections.Generic;
using System.Linq;
using Levels.data.model;
using Levels.domain;
using Levels.domain.model;
using Levels.domain.repositories;
using UnityEngine;
using Zenject;

namespace Levels.data
{
    public class LevelsRepository : ILevelsRepository, ILevelSceneObjectRepository
    {
        [Inject] private ILevelsDao levelsDao;
        [Inject] private ILevelCompletedStateDao completedStateDao;

        public List<Level> GetLevels() => levelsDao.GetLevelEntities().Select(GetLevel).ToList();

        public Level GetLevel(long levelId)
        {
            var index = Convert.ToInt32(levelId);
            var entity = GetById(levelId);
            return GetLevel(entity, index);
        }

        public void SetLevelCompleted(long levelId) => completedStateDao.SetCompleted(levelId);

        public GameObject GetLevelScene(long levelId) => GetById(levelId).scenePrefab;

        private Level GetLevel(LevelEntity entity, int number)
        {
            var id = Convert.ToInt64(number);
            var completed = completedStateDao.IsCompleted(id);
            return new Level(id, number, completed, entity.reward);
        }

        private LevelEntity GetById(long levelId)
        {
            var index = Convert.ToInt32(levelId);
            return levelsDao.GetLevelEntities()[index];
        }

        public interface ILevelsDao
        {
            public List<LevelEntity> GetLevelEntities();
        }
    }
}