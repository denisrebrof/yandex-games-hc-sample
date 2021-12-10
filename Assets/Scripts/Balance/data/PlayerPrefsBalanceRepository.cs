using Balance.domain;
using Balance.domain.repositories;
using UnityEngine;

namespace Balance.data
{
    public class PlayerPrefsBalanceRepository : IBalanceRepository
    {
        private const string PrefsKeyPrefix = "Balance";

        public int GetBalance() => PlayerPrefs.GetInt(PrefsKeyPrefix, 0);

        public void Add(int value)
        {
            var balance = GetBalance() + value;
            PlayerPrefs.SetInt(PrefsKeyPrefix, balance);
        }

        public void Remove(int value)
        {
            var removeResult = GetBalance() - value;
            var balance = Mathf.Max(0, removeResult);
            PlayerPrefs.SetInt(PrefsKeyPrefix, balance);
        }
    }
}