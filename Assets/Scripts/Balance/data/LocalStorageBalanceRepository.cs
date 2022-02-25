using System;
using Balance.domain.repositories;
using Plugins.FileIO;
using UniRx;
using UnityEngine;

namespace Balance.data
{
    public class LocalStorageBalanceRepository : IBalanceRepository
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
            LocalStorageIO.SetInt(PREFS_KEY_PREFIX, balance);
            LocalStorageIO.Save();
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
            LocalStorageIO.SetInt(PREFS_KEY_PREFIX, balance);
            LocalStorageIO.Save();
            balanceFlow.Value = balance;
        }

        private static int GetBalanceValue() => LocalStorageIO.GetInt(PREFS_KEY_PREFIX, 0);
    }
}