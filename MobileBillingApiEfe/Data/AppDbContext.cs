using Microsoft.EntityFrameworkCore;
using MobileBillingApiEfe.Models;

namespace MobileBillingApiEfe.Data
{
    public class AppDbContext : DbContext
    {

        public DbSet<Usage> Usages { get; set; }
        public DbSet<Bill> Bills { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
