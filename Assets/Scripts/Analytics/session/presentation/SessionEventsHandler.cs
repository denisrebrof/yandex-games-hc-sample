using Analytics.adapter;
using Analytics.levels;
using Analytics.session.domain;
using UnityEngine;
using Zenject;

namespace Analytics.session.presentation
{
    public class SessionEventsHandler : MonoBehaviour
    {
        [Inject] private AnalyticsAdapter analytics;
        [Inject] private ISessionEventLevelIdProvider levelIdProvider;

        private void Awake() => SendEvent(SessionEvent.Start);

        private void OnApplicationQuit() => SendEvent(SessionEvent.Quit);

        private void SendEvent(SessionEvent sessionEvent)
        {
            var levelId = levelIdProvider.GetCurrentLevelId();
            var pointer = new LevelPointer(levelId);
            analytics.SendSessionEvent(sessionEvent, pointer);
        }
    }
}