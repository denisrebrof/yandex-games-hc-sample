namespace Purchases.domain.model
{
    public class Purchase
    {
        public long Id;
        public string Name;
        public PurchaseType Type;

        public Purchase(long id, string name, PurchaseType type)
        {
            Id = id;
            Name = name;
            Type = type;
        }
    }
}