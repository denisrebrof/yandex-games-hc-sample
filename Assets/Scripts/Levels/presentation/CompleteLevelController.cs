using Levels.domain;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Levels.presentation
{
    public class CompleteLevelController: MonoBehaviour, ILevelCompletedListener
    {
        [SerializeField] private UnityAction onLevelCompleted;
        [SerializeField] private UnityAction<int> rewardListener;
        [Inject] private CompleteCurrentLevelUseCase completeCurrentLevelUseCase;
        
        public void CompleteCurrentLevel()
        {
            var completeResult = completeCurrentLevelUseCase.CompleteCurrentLevel();
            var reward = completeResult.CompletedLevel.Reward;
        }
    }
}