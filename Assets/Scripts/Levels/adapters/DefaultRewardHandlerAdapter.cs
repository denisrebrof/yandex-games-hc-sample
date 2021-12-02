using Balance.domain;
using Levels.domain;
using Zenject;

namespace Levels.adapters
{
    public class DefaultRewardHandlerAdapter: CompleteCurrentLevelUseCase.IRewardHandler
    {
        [Inject] private IBalanceRepository balanceRepository;
        public void HandleReward(int amount) => balanceRepository.Add(amount);
    }
}