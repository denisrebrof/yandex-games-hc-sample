using System;
using Levels.data.dao;
using Levels.domain.model;
using Levels.domain.repositories;
using Zenject;

namespace Levels.domain
{
    public class GetPreviousLevelUseCase
    {
        [Inject] private ICurrentLevelRepository currentLevelRepository;
        [Inject] private ILevelsRepository levelsRepository;

        private int GetCurrentLevelIndex()
        {
            var currentLevelId = currentLevelRepository.GetCurrentLevel().ID;
            return levelsRepository.GetLevels().FindIndex(level => level.ID == currentLevelId);
        }

        public bool GetHasPreviousLevel() => GetCurrentLevelIndex() > 0;
        
        public Level GetPreviousLevel()
        {
            var currentIndex = GetCurrentLevelIndex();
            return levelsRepository.GetLevels()[currentIndex-1];
        }
    }
}