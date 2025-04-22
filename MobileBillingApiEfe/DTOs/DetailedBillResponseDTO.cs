namespace MobileBillingApiEfe.DTOs
{
    public class DetailedBillResponseDTO
    {
        public int SubscriberId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public int TotalPhoneMinutes { get; set; }
        public int TotalInternetMb { get; set; }

        public decimal PhoneBill { get; set; }         
        public decimal InternetBill { get; set; }      
        public decimal TotalBill { get; set; }         

        public bool IsPaid { get; set; }
        public List<UsageDTO> Usages { get; set; } = new();


    }

}
