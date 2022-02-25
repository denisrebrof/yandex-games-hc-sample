using System;
using Balance.domain.repositories;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace Balance.presentation.ui
{
    public class ReactiveBalanceText: MonoBehaviour
    {
        [SerializeField] private Text text;
        [Inject] private IBalanceRepository balanceRepository;
        [SerializeField] private UnityEvent onUpdateText;

        private void Awake()
        {
            if (text == null)
                text = GetComponent<Text>();
        }

        private void Start() => balanceRepository.GetBalance().Subscribe(UpdateBalance).AddTo(this);

        private void UpdateBalance(int balance)
        {
            text.text = balance.ToString();
            onUpdateText.Invoke();
        }
    }
}