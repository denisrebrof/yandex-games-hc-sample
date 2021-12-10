using Balance.domain.repositories;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Balance.presentation.ui
{
    public class DefaultBalanceText: MonoBehaviour
    {
        [Inject] private IBalanceRepository balanceRepository;
        [SerializeField] private Text target;

        private void OnEnable() => UpdateBalanceText();

        public void UpdateBalanceText()
        {
            var balance = balanceRepository.GetBalance();
            target.text = balance.ToString();
        }
    }
}