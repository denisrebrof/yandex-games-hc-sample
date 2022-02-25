using System;

namespace SDK.GameState
{
    public interface IGameStateNavigator
    {
        IObservable<GameState> GetGameState();
    }
}
