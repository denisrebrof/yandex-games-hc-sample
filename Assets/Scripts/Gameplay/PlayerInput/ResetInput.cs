using UnityEngine;
using Zenject;

namespace Gameplay.PlayerInput
{
    public class ResetInput: MonoBehaviour
    {
        [Inject] private InputHandler handler;

        public void ResetAxis()
        {
            handler.Reset();
        }
    }
}