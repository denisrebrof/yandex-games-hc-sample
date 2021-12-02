using Zenject;

namespace Levels.domain
{
    public class CompleteCurrentLevelUseCase
    {
        [Inject] private ICurrentLevelRepository currentLevelRepository;
        [Inject] private IRewardHandler rewardHandler;
        [Inject] private ILastRewardRepository lastRewardRepository;
        [Inject] private SetNextCurrentLevelUseCase setNextCurrentLevelUseCase;

        public void CompleteCurrentLevel()
        {
            var currentLevel = currentLevelRepository.GetCurrentLevel();
            setNextCurrentLevelUseCase.SetNextCurrentLevel();
            rewardHandler.HandleReward(currentLevel.Reward);
            lastRewardRepository.Set(currentLevel.Reward);
        }

        public interface IRewardHandler
        {
            void HandleReward(int amount);
        }
    }
}