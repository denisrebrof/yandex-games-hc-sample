using System;

namespace SDK.PlayerData.domain
{
    public interface IPlayerIdRepository
    {
        public bool GetPlayerIdAvailable(); 
        public IObservable<string> InitializeWithPlayerId();
    }
}