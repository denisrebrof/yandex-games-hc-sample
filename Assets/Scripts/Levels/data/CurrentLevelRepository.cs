using System;
using Levels.domain;
using Levels.domain.model;
using Levels.domain.repositories;
using UniRx;
using Zenject;

namespace Levels.data
{
    class CurrentLevelRepository : ICurrentLevelRepository
    {
        [Inject] private ILevelsRepository levelsRepository;
        [Inject] private ICurrentLevelIdDao currentLevelIdDao;
        [Inject] private IDefaultLevelIdDao defaultLevelIdDao;

        private BehaviorSubject<Level> currentLevelSubject;

        public CurrentLevelRepository()
        {
            var currentLevel = GetCurrentLevel();
            currentLevelSubject = new BehaviorSubject<Level>(currentLevel);
        }

        public void SetCurrentLevel(long levelId)
        {
            currentLevelIdDao.SetCurrentLevelId(levelId);
            currentLevelSubject.OnNext(GetCurrentLevel());
        }

        public Level GetCurrentLevel()
        {
            var currentLevelId = currentLevelIdDao.HasCurrentLevelId()
                ? currentLevelIdDao.GetCurrentLevelId()
                : defaultLevelIdDao.GetDefaultLevelId();

            return levelsRepository.GetLevel(currentLevelId);
        }

        public IObservable<Level> GetCurrentLevelFlow() => currentLevelSubject;

        public interface ICurrentLevelIdDao
        {
            public bool HasCurrentLevelId();
            public long GetCurrentLevelId();
            public void SetCurrentLevelId(long id);
        }
        
        public interface IDefaultLevelIdDao
        {
            public long GetDefaultLevelId();
        }
    }
}