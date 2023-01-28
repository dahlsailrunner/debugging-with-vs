namespace CarvedRock.Core
{
    public class LocationInventory
    {
        public int ProductId { get; set; }
        public int LocationId { get; set; }
        public int OnHand { get; set; }
        public int OnOrder { get; set; }
    }

    public class Promotion
    {
        public int ProductId { get; set; }
        public string Description { get; set; } = string.Empty;
        public double Discount { get; set; }
    }

    public class LocalClaim
    {
        public string Type { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;

        public override string ToString() => $"{Type} : {Value}";
    }

}
