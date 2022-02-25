using UniRx;
using UnityEngine;
using UnityEngine.Events;

namespace Utils
{
    public class RigidbodyMovementListener : MonoBehaviour
    {
        [SerializeField] private Rigidbody target;
        [SerializeField] private float speedFactor = 1f;
        [SerializeField] private float idleValue = 1;
        [SerializeField] private float maxValue = 2;
        [SerializeField] private UnityEvent<float> onMove = new();

        private readonly ReactiveProperty<float> impact = new(0f);

        private void Start() => impact.Subscribe(onMove.Invoke).AddTo(this);

        private void Update()
        {
            if (target == null)
                return;

            impact.Value = Mathf.Clamp(idleValue + target.velocity.magnitude * speedFactor, idleValue, maxValue);
        }
    }
}