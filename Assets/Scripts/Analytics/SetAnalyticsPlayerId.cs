using Analytics.adapter;
using SDK.PlayerData.domain;
using UniRx;
using UnityEngine;
using Zenject;

namespace Analytics
{
    public class SetAnalyticsPlayerId : MonoBehaviour
    {
        [Inject] private AnalyticsAdapter analytics;
        [Inject] private IPlayerIdRepository playerIdRepository;

        private void Awake()
        {
            Debug.Log("GetPlayerIdAvailable: " + playerIdRepository.GetPlayerIdAvailable());
            if (!playerIdRepository.GetPlayerIdAvailable())
            {
                analytics.InitializeWithoutPlayerId();
                return;
            }

            SetupId();
        }

        private void SetupId() => playerIdRepository
            .InitializeWithPlayerId()
            .Subscribe(id => analytics.SetPlayerId(id))
            .AddTo(this);
    }
}