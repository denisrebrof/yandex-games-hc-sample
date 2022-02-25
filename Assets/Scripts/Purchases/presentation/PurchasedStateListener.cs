using Purchases.domain;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Purchases.presentation
{
    public class PurchasedStateListener : MonoBehaviour
    {
        [SerializeField] private long purchaseID = DEFAULT_PURCHASE_ID;
        [SerializeField] private UnityEvent onPurchased = new();
        [SerializeField] private UnityEvent onNotPurchased = new();

        [Inject] private PurchasedStateUseCase purchasedStateUseCase;
        private const long DEFAULT_PURCHASE_ID = -1;

        private void Start() => purchasedStateUseCase
            .GetPurchasedState(purchaseID)
            .Select(state => state ? onPurchased : onNotPurchased)
            .Subscribe(selected => selected.Invoke())
            .AddTo(this);
    }
}