namespace MobileBillingApiEfe.DTOs
{
    public class PayRequestDTO
    {
        public int SubscriberId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal Amount { get; set; } 

    }
}
