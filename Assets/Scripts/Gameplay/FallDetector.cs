using Doozy.Engine;
using UnityEngine;

namespace Gameplay
{
    public class FallDetector : MonoBehaviour
    {
        [SerializeField] private float fallHeight;
        [SerializeField] private Transform target;

        [SerializeField] private string startFallGameEvent = "Fall";

        private bool fallen = false;

        // Update is called once per frame
        void Update()
        {
            if (target.position.y <= fallHeight == fallen)
                return;

            fallen = !fallen;
            if (fallen)
                GameEventMessage.SendEvent(startFallGameEvent);
        }
    }
}