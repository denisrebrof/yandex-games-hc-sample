using System;

namespace Balance.presentation.ui
{
    public interface ICollectDailyRewardPresenter
    {
        void ShowDailyRewardCollection(int amount, Action animationCallback);
    }
}