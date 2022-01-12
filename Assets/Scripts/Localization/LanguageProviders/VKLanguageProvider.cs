namespace Localization.LanguageProviders
{
    public class VKLanguageProvider : ILanguageProvider
    {
        private Language defaultLanguage = Language.Russian;

        public Language GetCurrentLanguage() => defaultLanguage;
    }
}