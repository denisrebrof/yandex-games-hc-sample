using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Utils.Editor.SetupWizard
{
    public class SetupWizard : ScriptableWizard
    {
        private static readonly ProjectSettingsFileRepository<ProjectGlobalSettingsData> SettingsRepo =
            new GlobalSettingsRepository();

        public ProjectGlobalSettingsData data;

        /// <summary>
        /// Creating and displaying wizard.
        /// </summary>
        [MenuItem("Tools/Setup Wizard")]
        public static void CreateWizard()
        {
            var wizard = DisplayWizard<SetupWizard>("Setup Wizard", "Setup Project");
            var d = SettingsRepo.Get();
            wizard.data = d;
            Debug.Log("Data: " + d);
        }

        private void OnWizardCreate()
        {
            SetupInterstitialOnShow();
            SetupMetrikaId();
            SettingsRepo.Set(data);
        }

        public override void SaveChanges()
        {
            base.SaveChanges();
            SettingsRepo.Set(data);
        }

        private void SetupMetrikaId()
        {
            var path = Directory
                .GetFiles(FileUtils.ProjectRootPath)
                .First(fileName => fileName.Contains("insertHeadJsY"));
            
            var lines = File.ReadAllLines(path);
            
            var lineIndex = lines
                .ToList()
                .FindIndex(line =>
                    line.Contains("ym(")
                );

            lines[lineIndex] = "ym(" + data.metrikaCounterId + ", \"init\", {";
            
            lineIndex = lines
                .ToList()
                .FindIndex(line =>
                    line.Contains("<img src=\"https://mc.yandex.ru/watch/")
                );
            lines[lineIndex] = "<img src=\"https://mc.yandex.ru/watch/" + data.metrikaCounterId + "\" style=\"position:absolute; left:-9999px;\" alt=\"\" />";
            File.WriteAllLines(path, lines.ToArray());
        }

        private void SetupInterstitialOnShow()
        {
            var path = Directory
                .GetFiles(FileUtils.ProjectRootPath)
                .First(fileName => fileName.Contains("insertBodyJsY"));
            
            var lines = File.ReadAllLines(path);
            
            var lineIndex = lines
                .ToList()
                .FindIndex(line =>
                    line.Contains("sdk.adv.showFullscreenAdv({ callbacks: {} })")
                );

            var line = lines[lineIndex];
            lines[lineIndex] = data.yandexStartInterstitialEnabled ? UncommentJsLine(line) : CommentJsLine(line);

            File.WriteAllLines(path, lines.ToArray());
        }

        private static string CommentJsLine(string line)
        {
            if (line.StartsWith("<!--")) return line;
            return "<!--" + line + "-->";
        }

        private static string UncommentJsLine(string line)
        {
            if (!line.StartsWith("<!--")) return line;
            return line.Replace("<!--", "").Replace("-->", "");
        }
    }
}