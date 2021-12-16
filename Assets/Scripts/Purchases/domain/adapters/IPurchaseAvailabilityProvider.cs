namespace Purchases.domain.adapters
{
    public interface IPurchaseAvailabilityProvider
    {
        bool GetPurchaseAvailable(int price);
    }
}