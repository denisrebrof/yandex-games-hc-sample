using Levels.domain;

namespace Levels.adapters
{
    public class StubLevelRewardHandlerAdapter : CompleteCurrentLevelUseCase.IRewardHandler
    {
        public void HandleReward(int amount)
        {
        }
    }
}