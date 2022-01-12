using System;
using Sound.domain;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Zenject;

namespace Sound.presentation
{
    [RequireComponent(typeof(Toggle))]
    public class SimpleSoundToggle : MonoBehaviour
    {
        [Inject] private ISoundPrefsRepository repository;
        [SerializeField] private AudioMixer mixer;

        private Toggle toggle;

        private void Awake() => toggle = GetComponent<Toggle>();
        
        void Start()
        {
            var state = repository.GetSoundEnabledState();
            toggle.isOn = state;
            UpdateAudio(state);
            toggle.onValueChanged.AddListener(UpdateAudio);
        }

        private void UpdateAudio(bool state)
        {
            Debug.Log("UpdateAudio: " + state.ToString());
            mixer.SetFloat("MasterVolume", state ? 0 : -80);
        }
    }
}
