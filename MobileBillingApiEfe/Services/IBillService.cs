using MobileBillingApiEfe.DTOs;
using MobileBillingApiEfe.Models;

namespace MobileBillingApiEfe.Services
{
    public interface IBillService
    {
        Bill CalculateBill(BillRequestDTO dto);
        string PayBill(PayRequestDTO dto);
        List<Bill> GetBillsBySubscriber(int subscriberId);
        DetailedBillResponseDTO GetDetailedBill(int subscriberId, int month, int year, int page = 1, int pageSize = 10);
    }
}
