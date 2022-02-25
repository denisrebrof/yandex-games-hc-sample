using Analytics.adapter;
using Analytics.session.domain;
using UnityEngine;
using Zenject;

namespace Analytics.session.presentation
{
    public class FirstLaunchEventHandler : MonoBehaviour
    {
        [Inject] private IFirstOpenEventSentRepository firstOpenEventSentRepository;
        [Inject] private AnalyticsAdapter analytics;
        
        private void Start()
        {
            if(!firstOpenEventSentRepository.IsFirstOpen()) return;
            analytics.SendFirstOpenEvent();
            firstOpenEventSentRepository.SetFirstOpenAppeared();
        }
    }
}
