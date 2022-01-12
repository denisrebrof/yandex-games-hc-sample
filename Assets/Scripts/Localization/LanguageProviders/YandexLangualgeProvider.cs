namespace Localization.LanguageProviders
{
    public class YandexLanguageProvider: ILanguageProvider
    {
        private Language defaultLanguage = Language.English;
        
        public Language GetCurrentLanguage()
        {
#if YANDEX_SDK
            var lang = YandexSDK.instance.GetLanguage();
            return lang switch
            {
                "ru" => Language.Russian,
                "en" => Language.English,
                _ => defaultLanguage
            };
#endif
            return defaultLanguage;
        }
    }
}