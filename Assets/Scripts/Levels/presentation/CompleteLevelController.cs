using Doozy.Engine;
using Levels.domain;
using UnityEngine;
using Zenject;

namespace Levels.presentation
{
    public class CompleteLevelController : MonoBehaviour, ILevelCompletedListener
    {
        [SerializeField] private string uiEventName = "LevelCompleted";
        [Inject] private CompleteCurrentLevelUseCase completeCurrentLevelUseCase;

        public void CompleteCurrentLevel()
        {
            completeCurrentLevelUseCase.CompleteCurrentLevel();
            GameEventMessage.SendEvent(uiEventName);
        }
    }
}