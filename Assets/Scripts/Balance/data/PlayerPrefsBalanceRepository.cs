using System;
using Balance.domain.repositories;
using UniRx;
using UnityEngine;

namespace Balance.data
{
    public class PlayerPrefsBalanceRepository : IBalanceRepository
    {
        private const string PREFS_KEY_PREFIX = "Balance";

        private readonly IntReactiveProperty balanceFlow = new();

        public IObservable<int> GetBalance()
        {
            balanceFlow.Value = GetBalanceValue();
            return balanceFlow;
        }

        public void Add(int value)
        {
            var balance = GetBalanceValue() + value;
            PlayerPrefs.SetInt(PREFS_KEY_PREFIX, balance);
            try
            {
                balanceFlow.Value = balance;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Remove(int value)
        {
            var removeResult = GetBalanceValue() - value;
            var balance = Mathf.Max(0, removeResult);
            PlayerPrefs.SetInt(PREFS_KEY_PREFIX, balance);
            balanceFlow.Value = balance;
        }

        private static int GetBalanceValue() => PlayerPrefs.GetInt(PREFS_KEY_PREFIX, 0);
    }
}