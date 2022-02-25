using System;
using Analytics.session.domain;
using Plugins.FileIO;
using UnityEngine;

namespace Analytics.session.data
{
    public class LocalStorageFirstOpenEventSentRepository: IFirstOpenEventSentRepository
    {
        private const string PrefsKeyPrefix = "FirstOpenEventSent";
        public bool IsFirstOpen() => !LocalStorageIO.HasKey(PrefsKeyPrefix); 
        public void SetFirstOpenAppeared() => LocalStorageIO.SetString(PrefsKeyPrefix, "true");
    }
}