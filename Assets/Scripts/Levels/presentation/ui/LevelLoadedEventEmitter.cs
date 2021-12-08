using Doozy.Engine;
using UnityEngine;

namespace Levels.presentation.ui
{
    public class LevelLoadedEventEmitter : MonoBehaviour
    {
        [SerializeField] private string levelLoadedEvent = "LevelLoaded";

        public void Emit() => GameEventMessage.SendEvent(levelLoadedEvent);
    }
}