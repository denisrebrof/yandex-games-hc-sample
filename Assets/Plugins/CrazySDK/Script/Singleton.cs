// Singleton pattern: http://wiki.unity3d.com/index.php/Singleton

using UnityEngine;

namespace CrazyGames
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static bool m_ShuttingDown;
        private static readonly object m_Lock = new object();
        private static T m_Instance;

        public static T Instance
        {
            get
            {
                if (m_ShuttingDown) return null;

                lock (m_Lock)
                {
                    if (m_Instance != null) return m_Instance;

                    // Search for existing instance.
                    m_Instance = (T)FindObjectOfType(typeof(T));
                    // Create new instance if one doesn't already exist.
                    if (m_Instance != null) return m_Instance;
                    // Need to create a new GameObject to attach the singleton to.
                    var singletonObject = new GameObject();
                    m_Instance = singletonObject.AddComponent<T>();
                    singletonObject.name = typeof(T).ToString();

                    // Make instance persistent.
                    DontDestroyOnLoad(singletonObject);

                    return m_Instance;
                }
            }
        }

        private void OnDestroy()
        {
            m_ShuttingDown = true;
        }

        private void OnApplicationQuit()
        {
            m_ShuttingDown = true;
        }
    }
}