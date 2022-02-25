using Balance.domain.repositories;
using UniRx;
using UnityEngine;
using Zenject;

namespace Balance.presentation.utils
{
    public class SetBalanceDebugHandle : MonoBehaviour
    {
        [SerializeField] private int amount;
        [Inject] private IBalanceRepository balanceRepository;

        [ContextMenu("SetBalance")]
        public void SetBalance()
        {
            if (amount < 0)
                return;

            balanceRepository.GetBalance().Subscribe(balance =>
            {
                var difference = balance - amount;
                if (difference > 0)
                    balanceRepository.Remove(difference);
                else
                    balanceRepository.Add(-difference);
            });
        }
    }
}