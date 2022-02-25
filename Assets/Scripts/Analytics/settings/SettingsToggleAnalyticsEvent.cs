using Analytics.adapter;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Analytics.settings
{
    [RequireComponent(typeof(Toggle))]
    public class SettingsToggleAnalyticsEvent : MonoBehaviour
    {
        [Inject] private AnalyticsAdapter analytics;
        [SerializeField] private SettingType type = SettingType.SoundToggle;
        
        private void Start() => GetComponent<Toggle>().onValueChanged.AddListener(Toggle);
        private void Toggle(bool state) => analytics.SendSettingsEvent(type, state.ToString());
    }
}