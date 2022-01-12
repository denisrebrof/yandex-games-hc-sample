using System.Collections;
using UnityEngine;
using Zenject;

public class PokiHappyTime : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] private float intensity = 0.5f;
    [SerializeField] private float cooldown = 0.5f;

    private bool oncooldown = false;
    
#if POKI_SDK
    [Inject] private PokiUnitySDK sdk;
#endif

    public void HappyTime()
    {
#if POKI_SDK
        if(oncooldown)
            return;
        sdk.happyTime(intensity);
        StartCoroutine(Cooldown());
#endif
    }

    private IEnumerator Cooldown()
    {
        oncooldown = true;
        yield return new WaitForSeconds(cooldown);
        oncooldown = false;
    }
}