using Balance.domain;
using Balance.domain.repositories;
using Levels.domain;
using Zenject;

namespace Levels.adapters
{
    public class DefaultLevelRewardHandlerAdapter : CompleteCurrentLevelUseCase.IRewardHandler
    {
        [Inject] private IRewardRepository rewardRepository;

        public void HandleReward(int amount) => rewardRepository.Add(amount);
    }
}