using UnityEngine;

namespace Social
{
    public class SocialBlock : MonoBehaviour
    {
        private void Start()
        {
#if VK_SDK
        return;
#else
            DestroyImmediate(gameObject);
#endif
        }
    }
}