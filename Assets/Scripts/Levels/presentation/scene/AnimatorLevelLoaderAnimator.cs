using System;
using UnityEngine;

namespace Levels.presentation.scene
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorLevelLoaderAnimator : MonoBehaviour, LevelSceneLoader.ILevelLoadingTransition
    {
        [SerializeField] private string animatorTrigger = "SwitchLevel";
        private Animator cameraAnimator;
        private Action onHidden;
        private Action onComplete;

        private void Start() => cameraAnimator = GetComponent<Animator>();

        public void StartAnimation(Action onSceneHidden = null, Action onCompleted = null)
        {
            this.onHidden = onSceneHidden;
            this.onComplete = onCompleted;
            cameraAnimator.SetTrigger(animatorTrigger);
        }

        //Apply from animation
        public void OnSceneHidden() => onHidden?.Invoke();
        public void OnCompleted() => onComplete?.Invoke();
    }
}