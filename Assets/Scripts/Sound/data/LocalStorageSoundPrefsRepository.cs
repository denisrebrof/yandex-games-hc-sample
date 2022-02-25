using Plugins.FileIO;
using Sound.domain;
using UnityEngine;

namespace Sound.data
{
    public class LocalStorageSoundPrefsRepository: ISoundPrefsRepository
    {
        private string prefsKey = "soundEnabledState";
        
        public void SetSoundEnabledState(bool enabled)
        {
            LocalStorageIO.SetInt(prefsKey, enabled ? 1 : 0);
        }

        public bool GetSoundEnabledState()
        {
            return LocalStorageIO.GetInt(prefsKey, 1) == 1;
        }
    }
}