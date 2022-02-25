using System;
using Plugins.FileIO;
using SDK.PlayerData.domain;
using UniRx;

namespace SDK.PlayerData.data
{
    public class AnonimousPlayerIdRepository: IPlayerIdRepository
    {
        private const string PrefsKey = "AnonimousUserId";
        public bool GetPlayerIdAvailable() => true;

        public IObservable<string> InitializeWithPlayerId()
        {
            if(LocalStorageIO.HasKey(PrefsKey))
                return Observable.Return(LocalStorageIO.GetString(PrefsKey));

            var newGuid = System.Guid.NewGuid().ToString();
            LocalStorageIO.SetString(PrefsKey, newGuid);
            return Observable.Return(newGuid);
        }
    }
}