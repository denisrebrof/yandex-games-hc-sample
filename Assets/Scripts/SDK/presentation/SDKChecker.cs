using UnityEngine;
using UnityEngine.Events;

namespace SDK.presentation
{
    public class SDKChecker : MonoBehaviour
    {
        [SerializeField] private SDKProvider.SDKType type;
        [SerializeField] private UnityEvent onTypeMatches;
        [SerializeField] private UnityEvent onTypeNotMatches;

        void Awake()
        {
            var currentType = SDKProvider.GetSDK();
            if (currentType == type)
                onTypeMatches.Invoke();
            else
                onTypeNotMatches.Invoke();
        }
    }
}