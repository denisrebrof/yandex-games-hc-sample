using UnityEngine;

namespace CrazyGames
{
    class CrazySDKInit
    {
#if CRAZY_SDK
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
#endif
        static void OnRuntimeMethodLoad()
        {
            var sdk = CrazySDK.Instance; // Trigger init by calling instance
        }
    }
}