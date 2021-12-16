using DailyReward.domain;
using DailyReward.domain.model;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace DailyReward.presentation.ui
{
    public class AnimatorDailyRewardCollectingView : MonoBehaviour
    {
        [Inject] private DailyRewardStateUseCase stateUseCase;
        [Inject] private DailyRewardCollectUseCase collectRewardUseCase;

        [SerializeField] private Text amountText;
        [SerializeField] private GameObject cooldownStub;

        [Header("Animator")] [SerializeField] private Animator collectRewardAnimator;
        [SerializeField] private string collectRewardAnimatorTrigger = "collect";
        [SerializeField] private string closeScreenAnimatorTrigger = "close";

        public void Show()
        {
            gameObject.SetActive(true);
            var state = stateUseCase.GetCurrentRewardState();
            var prepared = state.CooldownState == DailyRewardCooldownState.Prepared;
            cooldownStub.SetActive(!prepared);
            if (!prepared)
                return;

            amountText.text = state.RewardAmount.ToString();
            collectRewardAnimator.SetTrigger(collectRewardAnimatorTrigger);
        }

        //Invoke from animator
        public void Collect() => collectRewardUseCase.Collect();

        public void Close() => collectRewardAnimator.SetTrigger(closeScreenAnimatorTrigger);
    }
}