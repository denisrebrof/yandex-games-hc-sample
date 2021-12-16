using Balance.domain.repositories;
using UnityEngine;
using Zenject;

namespace Balance.presentation.utils
{
    public class SetBalanceDebugHandle : MonoBehaviour
    {
        [SerializeField] private int amount;
        [Inject]
        private IBalanceRepository balanceRepository;

        [ContextMenu("SetBalance")]
        public void SetBalance()
        {
            if(amount<0)
                return;
            var balance = balanceRepository.GetBalance();
            var difference = balance - amount;
            if(difference>0)
                balanceRepository.Remove(difference);
            else
                balanceRepository.Add(-difference);
        }
    }
}