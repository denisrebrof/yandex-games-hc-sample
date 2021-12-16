using DailyReward.presentation.ui;
using UnityEngine;
using Zenject;

namespace DailyReward._di
{
    public class DailyRewardPresentationInstaller : MonoInstaller
    {
        [SerializeField] private AnimatorDailyRewardCollectingView animatorDailyRewardCollectingView;

        public override void InstallBindings()
        {
            //Presentation
            //UI
            Container
                .Bind<SimpleDailyRewardView.IDailyRewardViewCollectListener>()
                .To<AnimatorDailyRewardCollectorRewardCollectListenerAdapter>()
                .AsSingle();
            Container
                .Bind<AnimatorDailyRewardCollectingView>()
                .FromInstance(animatorDailyRewardCollectingView)
                .AsSingle();
        }
    }
}