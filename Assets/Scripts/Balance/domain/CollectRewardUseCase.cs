using Balance.domain.repositories;
using Zenject;

namespace Balance.domain
{
    public class CollectRewardUseCase
    {
        [Inject] private IBalanceRepository balanceRepository;
        [Inject] private IRewardRepository rewardRepository;

        public void Collect(float multiplier = 1f)
        {
            var collected = (int) (rewardRepository.Get() * multiplier);
            balanceRepository.Add(collected);
            rewardRepository.Drop();
        }
    }
}