using MobileBillingApiEfe.DTOs;

namespace MobileBillingApiEfe.Services
{
    public interface IUsageService
    {
        Task AddUsageAsync(UsageDTO dto);
    }
}
