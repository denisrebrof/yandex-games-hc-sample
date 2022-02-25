using System;
using SDK.GameState;
using UniRx;
using Unity.Collections;
using UnityEngine;

namespace Gameplay
{
    public class GameStateNavigator : MonoBehaviour, IGameStateNavigator
    {
        private ReactiveProperty<bool> menuShownState = new(false);
        private ReactiveProperty<bool> levelPlayingState = new(true);
        
        #if UNITY_EDITOR
        [SerializeField, ReadOnly] private bool menuShown = false;
        [SerializeField, ReadOnly] private bool levelPlaying = true;
        #endif

        public void SetMenuShownState(bool enabled)
        {
            menuShownState.Value = enabled;
#if UNITY_EDITOR
            menuShown = enabled;
#endif
        }

        public void SetLevelPlayingState(bool enabled)
        {
            levelPlayingState.Value = enabled;
#if UNITY_EDITOR
            levelPlaying = enabled;
#endif
        }

        public IObservable<GameState> GetGameState() => Observable.CombineLatest(
            menuShownState,
            levelPlayingState,
            (menuShown, levelPlaying) => !menuShown && levelPlaying
        ).Select(playing => playing ? GameState.Active : GameState.Disabled).DistinctUntilChanged();
    }
}