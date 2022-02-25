using Analytics;
using Analytics.adapter;
using Analytics.screens;
using Doozy.Engine.UI;
using UnityEngine;
using Zenject;

namespace Levels.presentation.analytics
{
    [RequireComponent(typeof(UIView))]
    public class ScreenStateAnalyticsHandler : MonoBehaviour
    {
        private UIView target;
        [Inject] private AnalyticsAdapter analytics;
        [SerializeField] private string screenName;

        private void Start()
        {
            target = GetComponent<UIView>();
            target.ShowBehavior.OnFinished.Event.AddListener(() => SendEvent(ScreenAction.Open));
            target.HideBehavior.OnFinished.Event.AddListener(() => SendEvent(ScreenAction.Close));
        }

        private void SendEvent(ScreenAction action) => analytics.SendScreenEvent(screenName, action);
    }
}