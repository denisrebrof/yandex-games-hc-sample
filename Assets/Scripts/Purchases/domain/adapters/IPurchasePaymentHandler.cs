namespace Purchases.domain.adapters
{
    public interface IPurchasePaymentHandler
    {

        public PurchasePaymentResult ExecutePayment(int price); 
            
        public enum PurchasePaymentResult
        {
            Success,
            Failure
        }
    }
}