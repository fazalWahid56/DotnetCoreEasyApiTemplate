using CoreTemplate.App.Db.Tables;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreTemplate.App.Db.Repositories
{
    public class VoucherTypeRepository : IVoucherTypeRepository
    {
        private readonly AppDbContext _context;

        public VoucherTypeRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<VoucherType> CreateAsync(VoucherType voucherType)
        {
            await _context.VoucherTypes.AddAsync(voucherType);
            await _context.SaveChangesAsync();
            return voucherType;
        }

        public async Task<bool> DeleteAsync(int voucherTypeId)
        {
            var account = await _context.VoucherTypes.FirstOrDefaultAsync(acc => acc.VoucherTypeId == voucherTypeId);
            if (account is not null)
            {
                account.IsDeleted = true;
                _context.Entry(account).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            throw new KeyNotFoundException("VoucherId not found.");
        }

        public async Task<List<VoucherType>> GetAllAsync()
        {
            var vouchers = await _context.VoucherTypes.Where(acc => acc.IsDeleted == false).ToListAsync();
            return vouchers;
        }

        public async Task<VoucherType> GetAsync(int voucherTypeId)
        {
            var account = await _context.VoucherTypes.FirstOrDefaultAsync(acc => acc.VoucherTypeId == voucherTypeId && acc.IsDeleted== false);
            if (account is not null)
            {
                return account;
            }
            throw new KeyNotFoundException("VoucherTypeId not found");
        }

        public async Task<VoucherType> UpdateAsync(VoucherType voucherType)
        {
            await _context.VoucherTypes.AddAsync(voucherType);
            _context.Entry(voucherType).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return voucherType;
        }
    }
}
