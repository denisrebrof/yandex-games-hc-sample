using System.Collections;
using UnityEngine;
using Zenject;

namespace SDK.HappyTime
{
    public class HappyTime : MonoBehaviour
    {
        [SerializeField, Range(0f, 1f)] private float intensity = 0.5f;
        [SerializeField] private float cooldown = 0.5f;

        private bool onCooldown = false;
    
        [Inject] private IHappyTimeController controller;

        public void SetHappyTime()
        {
            if(onCooldown)
                return;
            controller.SetHappyTime(intensity);
            StartCoroutine(Cooldown());
        }

        private IEnumerator Cooldown()
        {
            onCooldown = true;
            yield return new WaitForSeconds(cooldown);
            onCooldown = false;
        }
    }
}