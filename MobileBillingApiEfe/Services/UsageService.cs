using MobileBillingApiEfe.Data;
using MobileBillingApiEfe.DTOs;
using MobileBillingApiEfe.Models;
using Microsoft.EntityFrameworkCore;

namespace MobileBillingApiEfe.Services
{
    public class UsageService : IUsageService
    {
        private readonly AppDbContext _context;

        public UsageService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddUsageAsync(UsageDTO dto)
        {
            var usage = new Usage
            {
                SubscriberId = dto.SubscriberId,
                UsageType = dto.UsageType,
                Month = dto.Month,
                Year = dto.Year,
                Amount = dto.Amount
            };

            await _context.Usages.AddAsync(usage);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Usage>> GetUsagesAsync()
        {
            return await _context.Usages.ToListAsync();
        }
    }
}
