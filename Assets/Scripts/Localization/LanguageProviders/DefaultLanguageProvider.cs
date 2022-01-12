namespace Localization.LanguageProviders
{
    public class DefaultLanguageProvider: ILanguageProvider
    {
        public Language GetCurrentLanguage() => Language.English;
    }
}