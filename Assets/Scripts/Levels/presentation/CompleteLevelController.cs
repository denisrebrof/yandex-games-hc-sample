using Levels.domain;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Levels.presentation
{
    public class CompleteLevelController: MonoBehaviour, ILevelCompletedListener
    {
        [SerializeField] private UnityAction onLevelCompleted;
        [Inject] private CompleteCurrentLevelUseCase completeCurrentLevelUseCase;
        
        public void CompleteCurrentLevel() => completeCurrentLevelUseCase.CompleteCurrentLevel();
    }
}