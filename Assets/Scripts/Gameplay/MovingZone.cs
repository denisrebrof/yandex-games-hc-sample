using UnityEngine;

namespace Gameplay
{
    public class MovingZone : MonoBehaviour
    {
        private Transform prevParent = null;
        private void OnTriggerEnter(Collider col)
        {
            var transform1 = col.transform;
            prevParent = transform1.parent; 
            transform1.parent = transform;
        }

        private void OnTriggerExit(Collider col){
            col.transform.parent = prevParent;
        }
    }
}
