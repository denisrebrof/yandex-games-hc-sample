using UnityEditor;

namespace Utils.Editor.SetupWizard
{
    public class SetupWizard : ScriptableWizard
    {
        private ProjectSettingsFileRepository<ProjectGlobalSettingsData> settingsRepo = new GlobalSettingsRepository();
    
        public bool yandexInterstitialOnShow; 
    
        /// <summary>
        /// Creating and displaying wizard.
        /// </summary>
        [MenuItem("Tools/Setup Wizard")]
        public static void CreateWizard()
        {
            DisplayWizard<SetupWizard>("Setup Wizard", "Create");
        }

        private void OnWizardUpdate()
        {
        
        }
    }
}
