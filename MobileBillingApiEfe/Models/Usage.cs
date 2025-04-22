namespace MobileBillingApiEfe.Models
{
    public class Usage
    {
        public int Id { get; set; }
        public int SubscriberId { get; set; }
        public string UsageType { get; set; } = string.Empty; // phone ya da int
        public int Amount { get; set; } // 10 dakika veya 1 mb
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
