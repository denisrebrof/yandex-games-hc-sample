using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

public class SelectTemplate : IPreprocessBuildWithReport
{
    public int callbackOrder
    {
        get => 1;
    }

    public void OnPreprocessBuild(BuildReport report)
    {
        var currentTemplate = PlayerSettings.GetTemplateCustomValue("template");
        var templateValue = "Universal2020";
#if POKI_SDK
        templateValue = "poki-template";
#endif
        PlayerSettings.SetPropertyString("template", "PROJECT:" + templateValue, BuildTargetGroup.WebGL);
    }
}