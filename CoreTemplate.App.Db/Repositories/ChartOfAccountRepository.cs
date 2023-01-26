using CoreTemplate.App.Db.Tables;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreTemplate.App.Db.Repositories
{
    public class ChartOfAccountRepository : IChartOfAccountRepository
    {
        private readonly AppDbContext _context;

        public ChartOfAccountRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ChartOfAccount> CreateAsync(ChartOfAccount chartOfAccount)
        {
            await _context.ChartOfAccounts.AddAsync(chartOfAccount);
            await _context.SaveChangesAsync();
            return chartOfAccount;
         
        }

        public async Task<bool> DeleteAsync(int accountId)
        {
            var account = await _context.ChartOfAccounts.FirstOrDefaultAsync(acc => acc.AccountId == accountId);
            if (account is not null)
            {
                account.IsDeleted = true;
                _context.Entry(account).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            throw new KeyNotFoundException("AccountId not found.");
        }

        public async Task<List<ChartOfAccount>> GetAllAsync()
        {
            var accounts = await _context.ChartOfAccounts.Include(x=> x.AccountNature).ToListAsync();
            return accounts;
        }

        public async Task<ChartOfAccount> GetAsync(int accountId)
        {
            var account = await _context.ChartOfAccounts.Include(x => x.AccountNature).FirstOrDefaultAsync(acc => acc.AccountId == accountId && acc.IsDeleted==false);
            if (account is not null)
            {
                return account;
            }
            throw new KeyNotFoundException("AccountId not found");
        }

        public async Task<ChartOfAccount> UpdateAsync(ChartOfAccount chartOfAccount)
        {
            await _context.ChartOfAccounts.AddAsync(chartOfAccount);
            _context.Entry(chartOfAccount).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return chartOfAccount;
        }
    }
}
