using UnityEngine;

namespace Gameplay
{
    public class Restart : MonoBehaviour
    {
        [SerializeField] private Transform spawn;
        [SerializeField] private Rigidbody playerRigidbody;
        [SerializeField] private Transform Camera;

        public void Respawn()
        {
            playerRigidbody.velocity = Vector3.zero;
            var playerObject = playerRigidbody.transform;
            playerObject.position = spawn.position;
            playerObject.rotation = spawn.rotation;
            Camera.localRotation = Quaternion.identity;
        }
    }
}
