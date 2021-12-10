﻿using Levels.domain;
using Levels.domain.repositories;
using Purchases.domain;
using Zenject;

namespace Purchases.adapters
{
    public class LevelPassedStateProviderAdapter : PurchasedStateUseCase.ILevelPassedStateProvider
    {
        [Inject] private ILevelsRepository levelsRepository;
        public bool GetLevelPassedState(long levelId) => levelsRepository.GetLevel(levelId).CompletedState;
    }
}