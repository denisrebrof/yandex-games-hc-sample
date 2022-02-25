using UnityEngine;

namespace Gameplay
{
    public class DestroyGameObject : MonoBehaviour
    {
        [SerializeField] private GameObject target;

        private void Awake()
        {
            if (target == null)
                target = gameObject;
        }

        public void DestroyTarget() => Destroy(target);
    }
}