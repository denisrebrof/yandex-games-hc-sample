using Doozy.Engine;
using Levels.domain;
using UnityEngine;
using Zenject;

namespace Levels.presentation
{
    [CreateAssetMenu(fileName = "CompleteLevelController", menuName = "Levels/CompleteLevelController")]
    public class CompleteLevelController : ScriptableObject, ILevelCompletionHandler
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