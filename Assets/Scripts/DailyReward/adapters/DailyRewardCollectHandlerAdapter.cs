using Balance.domain.repositories;
using DailyReward.domain;
using Zenject;

namespace DailyReward.adapters
{
    public class DailyRewardCollectHandlerAdapter: DailyRewardCollectUseCase.IDailyRewardCollectHandler
    {
        [Inject] private IBalanceRepository balanceRepository;
        public void Handle(int amount) => balanceRepository.Add(amount);
    }
}