using Balance.domain.repositories;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace Balance.presentation.ui
{
    [RequireComponent(typeof(Text))]
    public class ReactiveBalanceText : MonoBehaviour
    {
        [Inject] private IBalanceRepository balanceRepository;
        [SerializeField] private UnityEvent onUpdate = new UnityEvent();
        private Text target;

        private void Awake()
        {
            target = GetComponent<Text>();
            balanceRepository.ObserveBalance().Subscribe(UpdateBalanceText).AddTo(this);
        }

        private void UpdateBalanceText(int balance)
        {
            target.text = balance.ToString();
            onUpdate.Invoke();
        }
    }
}