using Levels.domain.model;
using Zenject;

namespace Levels.domain
{
    public class CompleteCurrentLevelUseCase
    {
        [Inject] private ICurrentLevelRepository currentLevelRepository;
        [Inject] private IRewardHandler rewardHandler;
        [Inject] private ILastRewardRepository lastRewardRepository;
        [Inject] private SetNextCurrentLevelUseCase setNextCurrentLevelUseCase;

        public CompleteLevelResult CompleteCurrentLevel()
        {
            var currentLevel = currentLevelRepository.GetCurrentLevel();
            setNextCurrentLevelUseCase.SetNextCurrentLevel();
            var nextLevel = currentLevelRepository.GetCurrentLevel();
            rewardHandler.HandleReward(currentLevel.Reward);
            lastRewardRepository.Set(currentLevel.Reward);
            return new CompleteLevelResult(currentLevel, nextLevel);
        }

        public interface IRewardHandler
        {
            void HandleReward(int amount);
        }
    }
}