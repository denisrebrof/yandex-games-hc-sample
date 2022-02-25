using Doozy.Engine;
using UnityEngine;

namespace Utils
{
    public class GameEventMessageEmitter : MonoBehaviour
    {
        [SerializeField] private string message;

        public void Emit()
        {
            if(string.IsNullOrEmpty(message))
                return;
        
            GameEventMessage.SendEvent(message);
        }
    }
}
