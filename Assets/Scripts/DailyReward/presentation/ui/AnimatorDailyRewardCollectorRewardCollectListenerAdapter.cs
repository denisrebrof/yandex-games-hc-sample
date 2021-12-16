using UnityEngine;
using Zenject;

namespace DailyReward.presentation.ui
{
    public class AnimatorDailyRewardCollectorRewardCollectListenerAdapter: SimpleDailyRewardView.IDailyRewardViewCollectListener
    {
        [Inject] private AnimatorDailyRewardCollectingView collectScreen;
        public void ShowCollecting() => collectScreen.Show();
    }
}