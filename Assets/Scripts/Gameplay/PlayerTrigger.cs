using System;
using Doozy.Engine;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay
{
    public class PlayerTrigger : MonoBehaviour
    {
        [SerializeField] private UnityEvent triggerEvent;
        [SerializeField] private UnityEvent exitTriggerEvent;
        [SerializeField] private string gameEvent;
        [SerializeField] private string exitGameEvent;
        [SerializeField] private string triggerTag = "Player";

        [SerializeField] private bool triggerOnce = false;
        private bool triggeredEnter;
        private bool triggeredExit;

        private bool activeState;

        private void OnTriggerEnter(Collider other)
        {
            if(!other.CompareTag(triggerTag) || (triggerOnce && triggeredEnter))
                return;
        
            if (triggerEvent != null) triggerEvent.Invoke();
            if(!String.IsNullOrEmpty(gameEvent)) GameEventMessage.SendEvent(gameEvent);
            activeState = true;
            triggeredEnter = true;
        }

        private void OnDestroy()
        {
            if(activeState)
                ExitTrigger();
        }

        private void OnTriggerExit(Collider other)
        {
            if(!other.CompareTag(triggerTag) || (triggerOnce && triggeredExit))
                return;

            ExitTrigger();
            activeState = false;
            triggeredExit = true;
        }

        private void ExitTrigger()
        {
            if (exitTriggerEvent != null) exitTriggerEvent.Invoke();
            if (!String.IsNullOrEmpty(exitGameEvent)) GameEventMessage.SendEvent(exitGameEvent);
        }
    }
}