using System;
using Hints.domain;
using Localization;
using UnityEngine;
using Zenject;

namespace Hints.presentation
{
    public class Hint : MonoBehaviour
    {
        [Inject] private ICurrentHintRepository repository;
        [Inject] private ILanguageProvider languageProvider;
        [SerializeField,TextArea(3,10)] private string ru;
        [SerializeField,TextArea(3,10)] private string en;

        public void Setup()
        {
            string text;
            try
            {
                text = GetLocalizedText();
            }
            catch
            {
                text = ru;
            }

            repository.SetHintText(text);
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
}