using System;
using Levels.domain.repositories;
using Purchases.domain;
using Zenject;

namespace Purchases.adapters
{
    public class LevelPassedStateProviderAdapter : ILevelPassedStateProvider
    {
        [Inject] private ILevelCompletedStateRepository completedStateRepository;

        public IObservable<bool> GetLevelPassedState(long levelId) => completedStateRepository
            .GetLevelCompletedState(levelId);
    }
}