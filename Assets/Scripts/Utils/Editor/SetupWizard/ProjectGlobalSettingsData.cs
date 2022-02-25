using UnityEngine;

namespace Utils.Editor.SetupWizard
{
    [System.Serializable]
    public class ProjectGlobalSettingsData
    {
        [Header("Yandex")]
        [SerializeField] public bool yandexStartInterstitialEnabled = false;
        [SerializeField] public string metrikaCounterId;

        public ProjectGlobalSettingsData(bool yandexStartInterstitialEnabled, string metrikaCounterId)
        {
            this.yandexStartInterstitialEnabled = yandexStartInterstitialEnabled;
            this.metrikaCounterId = metrikaCounterId;
        }
    }
}