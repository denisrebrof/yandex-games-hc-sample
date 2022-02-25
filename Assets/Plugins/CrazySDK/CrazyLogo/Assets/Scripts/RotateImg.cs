using UnityEngine;
using UnityEngine.UI;

namespace Crazy
{
    [RequireComponent(typeof(Image))]
    public class RotateImg : MonoBehaviour
    {
        [SerializeField] private Vector3 axis = Vector3.forward;
        [SerializeField] private float speed = 1;

        private Image img;

        private void Start()
        {
            img = GetComponent<Image>();
        }

        private void Update()
        {
            img.rectTransform.Rotate(axis * (speed * Time.deltaTime));
        }
    }
}