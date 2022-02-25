using System;
using UnityEngine;

namespace Utils
{
    public class TransformScaler: MonoBehaviour
    {
        [SerializeField] private Transform target;

        private void Start()
        {
            if (target == null)
                target = transform;
        }

        public void SetScale(float scale)
        {
            target.localScale = Vector3.one*Math.Max(0f,scale);
        }
    }
}