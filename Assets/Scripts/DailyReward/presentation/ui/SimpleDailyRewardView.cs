using Balance.domain;
using DailyReward.domain;
using DailyReward.domain.model;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace DailyReward.presentation.ui
{
    public class SimpleDailyRewardView : MonoBehaviour
    {
        [Inject] private DailyRewardStateUseCase stateUseCase;
        [Inject] private DailyRewardCollectUseCase dailyRewardCollectRewardUse;
        [Inject] private IDailyRewardViewCollectListener collectListener;
        [SerializeField] private GameObject root;
        [SerializeField] private Text label;
        [SerializeField] private string readyText = "Ready";

        private void Awake()
        {
            stateUseCase.GetRewardState().Subscribe(RenderRewardState).AddTo(this);
        }

        private void RenderRewardState(DailyRewardState state)
        {
            var rewardDisabled = state.CooldownState == DailyRewardCooldownState.Disabled;
            root.SetActive(!rewardDisabled);
            if (rewardDisabled)
                return;

            var rewardPrepared = state.CooldownState == DailyRewardCooldownState.Prepared;
            var hours = state.TimeLeft.Hours;
            var minutes = state.TimeLeft.Minutes;
            var text = rewardPrepared ? readyText : $"{hours}:{minutes}";
        }

        public void OnClick()
        {
            var state = stateUseCase.GetCurrentRewardState();
            if (state.CooldownState == DailyRewardCooldownState.Disabled)
                return;

            collectListener.ShowCollecting();
        }

        public interface IDailyRewardViewCollectListener
        {
            void ShowCollecting();
        }
    }
}