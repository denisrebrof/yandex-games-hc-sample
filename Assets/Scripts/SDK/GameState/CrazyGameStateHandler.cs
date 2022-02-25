using System;
using UniRx;
using UnityEngine;
using Zenject;
#if CRAZY_SDK
using CrazyGames;
#endif

namespace SDK.GameState
{
    public class CrazyGameStateHandler : MonoBehaviour
    {
        [Inject] private IGameStateNavigator gameStateNavigator;

        private void Start() => gameStateNavigator.GetGameState().Subscribe(HandleGameState).AddTo(this);

        private void HandleGameState(GameState state)
        {
#if CRAZY_SDK
            switch (state)
            {
                case GameState.Active:
                    CrazySDK.Instance.GameplayStart();
                    break;
                case GameState.Disabled:
                    CrazySDK.Instance.GameplayStop();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
#endif
        }
    }
}