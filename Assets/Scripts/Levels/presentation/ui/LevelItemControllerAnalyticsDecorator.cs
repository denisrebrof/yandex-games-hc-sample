using Analytics;
using Analytics.adapter;
using Analytics.levels;
using Zenject;

namespace Levels.presentation.ui
{
    public class LevelItemControllerAnalyticsDecorator : LevelItem.ILevelItemController
    {
        private readonly LevelItem.ILevelItemController target;
        private readonly AnalyticsAdapter analytics;

        [Inject]
        public LevelItemControllerAnalyticsDecorator(
            LevelItem.ILevelItemController decorationTarget,
            AnalyticsAdapter analyticsAdapter
        )
        {
            target = decorationTarget;
            analytics = analyticsAdapter;
        }

        public void OnItemClick(long levelId)
        {
            analytics.SendLevelEvent(new LevelPointer(levelId), LevelEvent.Load);
            target.OnItemClick(levelId);
        }
    }
}