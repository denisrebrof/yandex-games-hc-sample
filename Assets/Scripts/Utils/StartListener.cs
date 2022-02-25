using UnityEngine;
using UnityEngine.Events;

namespace Utils
{
    public class StartListener : MonoBehaviour
    {
        [SerializeField] private UnityEvent onStart;
        void Start()
        {
            if(onStart!=null)
                onStart.Invoke();
        }
    }
}
