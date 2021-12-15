using Purchases.presentation.ui;
using UnityEngine;

namespace Purchases.adapters
{
    public class PurchaseItemSelectionAdapter: DefaultPurchaseItemController.IPurchaseItemSelectionAdapter
    {
        public void SelectItem(long purchaseId)
        {
            Debug.Log($"item selected: {purchaseId.ToString()}");
        }
    }
}