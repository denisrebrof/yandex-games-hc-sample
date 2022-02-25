using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class PlateController : MonoBehaviour
    {
        [SerializeField] private Animator target;
        [SerializeField] private string param = "DestructionState";
        [SerializeField] private float stepDuration = 1f;
        private int destructionState = 0;

        public void StartDestruction()
        {
            StartCoroutine(Destruction());
        }

        private IEnumerator Destruction()
        {
            while (true)
            {
                destructionState += 1;
                target.SetInteger(param, destructionState);
                yield return new WaitForSeconds(stepDuration);
            }
        }

        public void StopDestruction()
        {
            StopAllCoroutines();
        }

        public void DestroyMe()
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }

        public void Reset()
        {
            StopDestruction();
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
            target.Rebind();
        }
    }
}
