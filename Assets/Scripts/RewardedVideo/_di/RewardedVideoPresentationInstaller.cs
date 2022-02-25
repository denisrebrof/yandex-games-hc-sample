using RewardedVideo.domain;
using RewardedVideo.presentation;
using UnityEngine;
using Zenject;

namespace RewardedVideo._di
{
    public class RewardedVideoPresentationInstaller: MonoInstaller
    {
        [SerializeField] private YandexRewardedVideoNavigator navigator;
                
            public override void InstallBindings()
            {
                //Presenters
                Container
                    .Bind<IRewardedVideoNavigator>()
                    .To<StubRewardedVideoNavigator>()
                    .AsSingle();
            }
    }
}