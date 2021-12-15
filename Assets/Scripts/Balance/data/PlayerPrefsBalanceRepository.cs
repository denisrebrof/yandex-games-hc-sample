using System;
using Balance.domain;
using Balance.domain.repositories;
using UniRx;
using UnityEngine;

namespace Balance.data
{
    public class PlayerPrefsBalanceRepository : IBalanceRepository
    {
        private const string PrefsKeyPrefix = "Balance";
        
        private ReactiveProperty<int> balanceFlow = new ReactiveProperty<int>(PlayerPrefs.GetInt(PrefsKeyPrefix, 0));

        public int GetBalance() => PlayerPrefs.GetInt(PrefsKeyPrefix, 0);
        public IObservable<int> ObserveBalance() => balanceFlow;

        public void Add(int value)
        {
            var balance = GetBalance() + value;
            PlayerPrefs.SetInt(PrefsKeyPrefix, balance);
            balanceFlow.Value = GetBalance();
        }

        public void Remove(int value)
        {
            var removeResult = GetBalance() - value;
            var balance = Mathf.Max(0, removeResult);
            PlayerPrefs.SetInt(PrefsKeyPrefix, balance);
            balanceFlow.Value = GetBalance();
        }
    }
}