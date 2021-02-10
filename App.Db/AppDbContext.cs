using App.Db.Tables;
using Microsoft.EntityFrameworkCore;

namespace App.Db
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<AccountNature> AccountNature { get; set; }
        public DbSet<ChartOfAccount> ChartOfAccounts { get; set; }
        public DbSet<VoucherType> VoucherTypes { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<GeneralLedger> GeneralLedger { get; set; }

    }
}
