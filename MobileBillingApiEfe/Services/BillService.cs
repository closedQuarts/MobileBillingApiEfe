using MobileBillingApiEfe.DTOs;
using MobileBillingApiEfe.Models;
using MobileBillingApiEfe.Data;
using Microsoft.EntityFrameworkCore;

namespace MobileBillingApiEfe.Services
{
    public class BillService : IBillService
    {
        private readonly AppDbContext _context;

        public BillService(AppDbContext context)
        {
            _context = context;
        }

        public DetailedBillResponseDTO GetDetailedBill(int subscriberId, int month, int year, int page = 1, int pageSize = 10)
        {
            var usages = _context.Usages
                .Where(u => u.SubscriberId == subscriberId && u.Month == month && u.Year == year)
                .ToList();

            int totalPhone = usages
                .Where(u => string.Equals(u.UsageType, "phone", StringComparison.OrdinalIgnoreCase))
                .Sum(u => u.Amount);

            int totalInternet = usages
                .Where(u => string.Equals(u.UsageType, "internet", StringComparison.OrdinalIgnoreCase))
                .Sum(u => u.Amount);

            decimal phoneBill = 0;
            if (totalPhone > 1000)
                phoneBill = ((totalPhone - 1000) / 1000) * 10;

            decimal internetBill = 0;
            if (totalInternet > 0)
            {
                internetBill += 50;
                if (totalInternet > 20480)
                {
                    int extra = totalInternet - 20480;
                    internetBill += (extra / 10240) * 10;
                }
            }

            


            var bill = _context.Bills.FirstOrDefault(b =>
                b.SubscriberId == subscriberId &&
                b.Month == month &&
                b.Year == year);

            decimal totalBill = bill?.TotalBill ?? (phoneBill + internetBill);


            var pagedUsages = usages
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new UsageDTO
                {
                    SubscriberId = u.SubscriberId,
                    UsageType = u.UsageType,
                    Month = u.Month,
                    Year = u.Year,
                    Amount = u.Amount
                }).ToList();

            return new DetailedBillResponseDTO
            {
                SubscriberId = subscriberId,
                Month = month,
                Year = year,
                TotalPhoneMinutes = totalPhone,
                TotalInternetMb = totalInternet,
                PhoneBill = phoneBill,
                InternetBill = internetBill,
                TotalBill = bill?.TotalBill ?? phoneBill + internetBill,
                IsPaid = bill?.IsPaid ?? false,
                Usages = pagedUsages
            };
        }


        public Bill CalculateBill(BillRequestDTO dto)
        {
            var userUsages = _context.Usages
                .Where(u => u.SubscriberId == dto.SubscriberId &&
                            u.Month == dto.Month &&
                            u.Year == dto.Year)
                .ToList();

            int totalPhone = userUsages
                .Where(u => string.Equals(u.UsageType, "phone", StringComparison.OrdinalIgnoreCase))
                .Sum(u => u.Amount);

            int totalInternet = userUsages
                .Where(u => string.Equals(u.UsageType, "internet", StringComparison.OrdinalIgnoreCase))
                .Sum(u => u.Amount);

            // ilk 1000 free etc.
            decimal phoneBill = 0;
            if (totalPhone > 1000)
                phoneBill = ((totalPhone - 1000) / 1000) * 10;

            decimal internetBill = 0;
            if (totalInternet > 0)
            {
                internetBill += 50;
                if (totalInternet > 20480)
                {
                    int extra = totalInternet - 20480;
                    internetBill += (extra / 10240) * 10;
                }
            }

            decimal totalBill = phoneBill + internetBill;

            var bill = new Bill
            {
                SubscriberId = dto.SubscriberId,
                Month = dto.Month,
                Year = dto.Year,
                TotalBill = totalBill,
                IsPaid = false
            };

            var existing = _context.Bills.FirstOrDefault(b =>
                b.SubscriberId == bill.SubscriberId &&
                b.Month == bill.Month &&
                b.Year == bill.Year);

            if (existing == null)
            {
                _context.Bills.Add(bill);
                _context.SaveChanges();
            }

            return bill;
        }

        public string PayBill(PayRequestDTO dto)
        {
            var bill = _context.Bills.FirstOrDefault(b =>
                b.SubscriberId == dto.SubscriberId &&
                b.Month == dto.Month &&
                b.Year == dto.Year);

            if (bill == null)
                return "Fatura bulunamadı.";

            if (bill.IsPaid)
                return "Bu fatura zaten tamamen ödenmiş.";

            if (dto.Amount <= 0)
                return "Geçersiz ödeme miktarı.";

            if (dto.Amount >= bill.TotalBill)
            {
                bill.TotalBill = 0;
                bill.IsPaid = true;
                _context.SaveChanges();
                return "Fatura tamamen ödendi.";
            }
            else
            {
                bill.TotalBill -= dto.Amount;
                bill.IsPaid = false;
                _context.SaveChanges();
                return $"Kısmi ödeme alındı. Kalan borç: {bill.TotalBill}₺";
            }
        }

        public List<Bill> GetBillsBySubscriber(int subscriberId)
        {
            return _context.Bills
                .Where(b => b.SubscriberId == subscriberId)
                .ToList();
        }
    }
}
