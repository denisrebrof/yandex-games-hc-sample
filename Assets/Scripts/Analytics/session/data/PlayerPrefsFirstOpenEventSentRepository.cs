using System;
using Analytics.session.domain;
using UnityEngine;

namespace Analytics.session.data
{
    public class PlayerPrefsFirstOpenEventSentRepository: IFirstOpenEventSentRepository
    {
        private const string PrefsKeyPrefix = "FirstOpenEventSent";
        public bool IsFirstOpen() => !PlayerPrefs.HasKey(PrefsKeyPrefix);
        public void SetFirstOpenAppeared() => PlayerPrefs.SetString(PrefsKeyPrefix, "true");
    }
}