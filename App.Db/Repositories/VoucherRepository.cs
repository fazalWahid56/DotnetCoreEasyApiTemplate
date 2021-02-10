using App.Db.Tables;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Db.Repositories
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly AppDbContext _context;

        public VoucherRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Voucher> CreateAsync(Voucher voucher)
        {
            await _context.Vouchers.AddAsync(voucher);
            await _context.SaveChangesAsync();
            return voucher;
        }

        public async Task<bool> DeleteAsync(int voucherId)
        {
            var account = await _context.Vouchers.FirstOrDefaultAsync(acc => acc.VoucherId == voucherId);
            if (account is not null)
            {
                account.IsDeleted = true;
                _context.Entry(account).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            throw new KeyNotFoundException("VoucherId not found.");
        }

        public async Task<List<Voucher>> GetAllAsync()
        {
            var accounts = await _context.Vouchers.Where(acc => acc.IsDeleted == false).ToListAsync();
            return accounts;
        }

        public async Task<Voucher> GetAsync(int voucherId)
        {
            var account = await _context.Vouchers.FirstOrDefaultAsync(acc => acc.VoucherId == voucherId && acc.IsDeleted== false);
            if (account is not null)
            {
                return account;
            }
            throw new KeyNotFoundException("VoucherId not found");
        }

        public async Task<Voucher> UpdateAsync(Voucher voucher)
        {
            await _context.Vouchers.AddAsync(voucher);
            _context.Entry(voucher).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return voucher;
        }
    }
}
