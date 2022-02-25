using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Sound.presentation
{
    [RequireComponent(typeof(AudioSource))]
    public class DownloadAndPlayAudio : MonoBehaviour
    {
        public string url;
        public string editorUrl;
        public AudioSource source;

        IEnumerator Start()
        {
            source = GetComponent<AudioSource>();
            var targetUrl = url;
#if UNITY_EDITOR
            targetUrl = editorUrl;
#endif
            Debug.Log("Start Loading Audio");
            UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(targetUrl);
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error Loading Audio");
                Debug.LogError(www.error);
                yield break;
            }

            try
            {
                Debug.Log("End Loading Audio");
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
                source.clip = bundle.LoadAsset<AudioClip>("Music");
                source.Play();
                Debug.Log("Play Loading Audio");
            }
            catch (Exception e)
            {
                Debug.Log("Error Play Audio");
                Debug.LogError(e);
            }
        }
    }
}