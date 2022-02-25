using System.IO;
using UnityEngine;

namespace Utils.Editor.SetupWizard
{
    public abstract class ProjectSettingsFileRepository<T>
    {
        protected abstract string SettingsFileName { get; }
        
        protected abstract T DefaultSettingsInstance { get; }

        private string SettingsFilePath => Path.Combine(FileUtils.ProjectRootPath, SettingsFileName);


        public T Get()
        {
            Debug.Log("Get");
            Debug.Log("Exists: " + File.Exists(SettingsFilePath));
            if (!File.Exists(SettingsFilePath))
                return DefaultSettingsInstance;

            var content = File.ReadAllText(SettingsFilePath);
            Debug.Log("content: " + content);
            return JsonUtility.FromJson<T>(content);
        }

        public void Set(T settings)
        {
            var content = JsonUtility.ToJson(settings);
            File.WriteAllText(SettingsFilePath, content);
        }
    }
}