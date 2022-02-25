using System;
#if UNITY_WEBGL && !UNITY_EDITOR
using System.Runtime.InteropServices;
#endif
using UnityEngine;

namespace Plugins.FileIO
{
    public static class LocalStorageIO
    {
        public static void DeleteKey(string key)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            RemoveFromLocalStorage(key: key);
#else
        PlayerPrefs.DeleteKey(key: key);
#endif
        }

        public static bool HasKey(string key)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            return (HasKeyInLocalStorage(key) == 1);
#else
        return (PlayerPrefs.HasKey(key: key));
#endif
        }

        public static string GetString(string key)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            return LoadFromLocalStorage(key: key);
#else
        return (PlayerPrefs.GetString(key: key));
#endif
        }

        public static void SetString(string key, string value)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            SaveToLocalStorage(key: key, value: value);
#else
        PlayerPrefs.SetString(key: key, value: value);
#endif
        }

        public static void SetInt(string key, int value) => SetString(key, value.ToString());

        public static int GetInt(string key) => Convert.ToInt16(
            GetString(key)
        );

        public static int GetInt(string key, int def)
        {
            if (!HasKey(key))
                return def;
            return GetInt(key);
        }

        public static void Save()
        {
#if !UNITY_WEBGL
            PlayerPrefs.Save();
#endif
        }

        
#if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void SaveToLocalStorage(string key, string value);

        [DllImport("__Internal")]
        private static extern string LoadFromLocalStorage(string key);

        [DllImport("__Internal")]
        private static extern void RemoveFromLocalStorage(string key);

        [DllImport("__Internal")]
        private static extern int HasKeyInLocalStorage(string key);
#endif
    }
}