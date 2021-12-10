using Purchases.domain.model;

namespace Purchases.presentation.ui
{
    public interface IPurchaseItemFactory
    {
        PurchaseItem Create(PurchaseType type);
    }
}