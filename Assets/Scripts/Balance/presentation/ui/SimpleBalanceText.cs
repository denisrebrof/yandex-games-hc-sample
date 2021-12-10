using Balance.domain;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Balance.presentation.ui
{
    [RequireComponent(typeof(Text))]
    public class SimpleBalanceText : MonoBehaviour
    {
        [Inject] private IBalanceRepository balanceRepository;
        private Text target;

        private void Awake() => target = GetComponent<Text>();

        private void OnEnable()
        {
            var balance = balanceRepository.GetBalance();
            target.text = balance.ToString();
        }
    }
}