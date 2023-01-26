using CoreTemplate.App.Db.Tables;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreTemplate.App.Db.Repositories
{
    public class GeneralLedgerRepository : IGeneralLedgerRepository
    {
        private readonly AppDbContext _context;

        public GeneralLedgerRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<GeneralLedger> CreateAsync(GeneralLedger generalLedger)
        {
            await _context.GeneralLedger.AddAsync(generalLedger);
            await _context.SaveChangesAsync();
            return generalLedger;
        }

        public async Task<bool> DeleteAsync(int transectionId)
        {
            var account = await _context.GeneralLedger.FirstOrDefaultAsync(acc => acc.TransectionId == transectionId);
            if (account is not null)
            {
                account.IsDeleted = true;
                _context.Entry(account).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            throw new KeyNotFoundException("TransectionId not found.");
        }

        public async Task<List<GeneralLedger>> GetAllAsync()
        {
            var accounts = await _context.GeneralLedger.Where(acc => acc.IsDeleted == false).ToListAsync();
            return accounts;
        }

        public async Task<GeneralLedger> GetAsync(int transectionId)
        {
            var account = await _context.GeneralLedger.Include(x => x.FromAccount).Where(x => x.IsDeleted==false)
                                                      .Include(x => x.AgainstAccount).Where(x => x.IsDeleted == false)
                                                      .Include(x => x.Voucher).Where(x => x.IsDeleted == false)
                                                      .FirstOrDefaultAsync(acc => acc.TransectionId == transectionId && acc.IsDeleted == false);
                                
            if (account is not null)
            {
                return account;
            }
            throw new KeyNotFoundException("TransactionId not found");
        }

        public async Task<GeneralLedger> UpdateAsync(GeneralLedger generalLedger)
        {
            await _context.GeneralLedger.AddAsync(generalLedger);
            _context.Entry(generalLedger).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return generalLedger;
        }
    }
}
