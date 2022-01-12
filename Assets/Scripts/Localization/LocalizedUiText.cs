using Localization;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Text))]
public class LocalizedUiText : MonoBehaviour
{
    [Inject] private ILanguageProvider languageProvider;
    [SerializeField, TextArea(3, 10)] private string ru;
    [SerializeField, TextArea(3, 10)] private string en;

    private void Start()
    {
        var text = GetComponent<Text>();
        string local;
        try
        {
            local = GetLocalizedText();
        }
        catch
        {
            local = ru;
        }

        text.text = local;
    }

    private string GetLocalizedText()
    {
        var lang = languageProvider.GetCurrentLanguage();
        return lang switch
        {
            Language.Russian => ru,
            Language.English => en,
            _ => en
        };
    }
}