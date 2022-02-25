using System;
using Sound.domain;
using UniRx;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace Ads.presentation.InterstitialAdNavigator.decorators
{
    public class InterstitialAdNavigatorMuteAudioDecorator : MonoBehaviour, IInterstitalAdNavigator
    {
        [Inject] private IInterstitalAdNavigator target;
        [Inject] private ISoundPrefsRepository soundPrefsRepository;
        [SerializeField] private AudioMixer mixer;

        public IObservable<ShowInterstitialResult> ShowAd()
        {
            mixer.SetFloat("MasterVolume", -80);
            return target.ShowAd().DoOnCompleted(
                () =>
                {
                    var state = soundPrefsRepository.GetSoundEnabledState();
                    mixer.SetFloat("MasterVolume", state ? 0 : -80);
                }
            );
        }
    }
}