using System.Collections;
using UnityEngine;

namespace Utils
{
    public class ScaleTweener : MonoBehaviour
    {
        [SerializeField] private AnimationCurve curve = new();
        [SerializeField] private float duration = 1f;

        public void Tween()
        {
            StopAllCoroutines();
            if (gameObject.activeInHierarchy)
                StartCoroutine(TweenCoroutine());
        }

        private IEnumerator TweenCoroutine()
        {
            var timer = duration;
            while (timer > 0)
            {
                transform.localScale = Vector3.one * curve.Evaluate(1f - timer / duration);
                timer -= Time.deltaTime;
                yield return null;
            }

            transform.localScale = Vector3.one * curve.Evaluate(1f);
        }

        private void OnEnable()
        {
            transform.localScale = Vector3.one * curve.Evaluate(0f);
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }
    }
}