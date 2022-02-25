namespace Purchases.domain.model
{
    public class Purchase
    {
        public long Id;
        public string Name;
        public string Description;
        public PurchaseType Type;

        public Purchase(long id, string name, PurchaseType type, string description)
        {
            Id = id;
            Name = name;
            Type = type;
            Description = description;
        }
    }
}