using UnityEngine;

namespace Gameplay
{
    public class SlerpToLookAt : MonoBehaviour
    {
        //values that will be set in the Inspector
        public Transform Target;
        public float RotationSpeed;

        //values for internal use
        private Quaternion _lookRotation;
        private Vector3 _direction;

        // Update is called once per frame
        void Update()
        {
            //find the vector pointing from our position to the target
            _direction = (Target.position - transform.position).normalized;

            //create the rotation we need to be in to look at the target
            _lookRotation = Quaternion.LookRotation(_direction);

            //rotate us over time according to speed until we are in the required rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
        }
    }
}