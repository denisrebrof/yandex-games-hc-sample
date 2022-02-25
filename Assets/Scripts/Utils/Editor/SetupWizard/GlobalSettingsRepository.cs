namespace Utils.Editor.SetupWizard
{
    public class GlobalSettingsRepository: ProjectSettingsFileRepository<ProjectGlobalSettingsData>
    {
        protected override string SettingsFileName => "global_settings.json";
        protected override ProjectGlobalSettingsData DefaultSettingsInstance => new ProjectGlobalSettingsData();
    }
}