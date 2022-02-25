using Sound.domain;
using UnityEngine;

namespace Sound.data
{
    public class PlayerPrefsSoundPrefsRepository: ISoundPrefsRepository
    {
        private string prefsKey = "soundEnabledState";
        
        public void SetSoundEnabledState(bool enabled) => PlayerPrefs.SetInt(prefsKey, enabled ? 1 : 0);

        public bool GetSoundEnabledState()
        {
            return PlayerPrefs.GetInt(prefsKey, 1) == 1;
        }
    }
}