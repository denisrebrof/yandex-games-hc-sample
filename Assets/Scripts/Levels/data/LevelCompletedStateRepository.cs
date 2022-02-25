using System;
using System.Collections.Generic;
using Levels.domain.repositories;
using UniRx;
using Zenject;

namespace Levels.data
{
    public class LevelCompletedStateRepository: ILevelCompletedStateRepository
    {
        [Inject] private ILevelCompletedStateDao completedStateDao;
        
        private readonly Dictionary<long, ReactiveProperty<bool>> completedStateProcessors = new();
        
        public IObservable<bool> GetLevelCompletedState(long levelId)
        {
            if (completedStateProcessors.ContainsKey(levelId))
                return completedStateProcessors[levelId];
            
            var completedState = GetLevelCompletedStateValue(levelId);
            if(completedState)
                return Observable.Return(true);
            
            var processor = new ReactiveProperty<bool>(false);
            completedStateProcessors[levelId] = processor;
            return processor;
        }

        public bool GetLevelCompletedStateValue(long levelId) => completedStateDao.IsCompleted(levelId);

        public void SetLevelCompleted(long levelId)
        {
            completedStateDao.SetCompleted(levelId);
            if (!completedStateProcessors.ContainsKey(levelId)) return;
            var processor = completedStateProcessors[levelId];
            processor.Value = true;
            processor.Dispose();
            completedStateProcessors.Remove(levelId);
        }
    }
}