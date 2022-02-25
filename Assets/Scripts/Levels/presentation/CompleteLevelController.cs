using Doozy.Engine;
using Levels.domain;
using UnityEngine;
using Zenject;

namespace Levels.presentation
{
    public class CompleteLevelController : ILevelCompletionHandler
    {
        private readonly string uiEventName;
        [Inject] private CompleteCurrentLevelUseCase completeCurrentLevelUseCase;

        public CompleteLevelController(string uiEventName = "LevelCompleted")
        {
            this.uiEventName = uiEventName;
        }

        public void CompleteCurrentLevel()
        {
            completeCurrentLevelUseCase.CompleteCurrentLevel();
            GameEventMessage.SendEvent(uiEventName);
        }
    }
}