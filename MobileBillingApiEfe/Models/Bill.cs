using System.ComponentModel.DataAnnotations;

namespace MobileBillingApiEfe.Models
{
    public class Bill
    {
        [Key]
        public int Id { get; set; }
        public int SubscriberId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal TotalBill { get; set; }
        public bool IsPaid { get; set; } = false;
    }
}
