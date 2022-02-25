using Analytics;
using Analytics.adapter;
using Analytics.levels;
using Levels.domain.repositories;
using UnityEngine;
using Zenject;

namespace Levels.presentation.analytics
{
    public class LevelCompletedAnalyticsEvent : MonoBehaviour
    {
        [Inject] private AnalyticsAdapter analytics;
        [Inject] private ICurrentLevelRepository currentLevelRepository;

        public void Send()
        {
            var levelId = currentLevelRepository.GetCurrentLevel().ID;
            analytics.SendLevelEvent(new LevelPointer(levelId), LevelEvent.Complete);
        }
    }
}