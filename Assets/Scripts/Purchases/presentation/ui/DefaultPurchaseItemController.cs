using System;

namespace Purchases.presentation.ui
{
    public class DefaultPurchaseItemController: PurchaseItem.IPurchaseItemController
    {
        public void OnItemClick(long purchaseId)
        {
            throw new System.NotImplementedException();
        }

        public IObservable<bool> GetPurchasedState(long purchaseId)
        {
            throw new NotImplementedException();
        }
    }
}