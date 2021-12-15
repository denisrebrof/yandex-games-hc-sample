using RewardedVideo.domain;
using RewardedVideo.presentation;
using UnityEngine;
using Zenject;

namespace RewardedVideo._di
{
    public class RewardedVideoPresentationInstaller: MonoInstaller
    {
        [SerializeField] private YandexRewardedVideoPresenter presenter;
                
            public override void InstallBindings()
            {
                //Presenters
                Container
                    .Bind<IRewardedVideoPresenter>()
                    .FromInstance(presenter)
                    .AsSingle();
            }
    }
}