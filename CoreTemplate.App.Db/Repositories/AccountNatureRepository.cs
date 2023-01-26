using CoreTemplate.App.Db.Tables;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreTemplate.App.Db.Repositories
{
    public class AccountNatureRepository : IAccountNatureRepository
    {
        private readonly AppDbContext _context;

        public AccountNatureRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<AccountNature> CreateAsync(AccountNature accountNature)
        {
            await _context.AccountNature.AddAsync(accountNature);
            await _context.SaveChangesAsync();
            return accountNature;
        }

        public async Task<bool> DeleteAsync(int accountNatureId)
        {
            var account = await _context.AccountNature.FirstOrDefaultAsync(acc => acc.AccountNatureId == accountNatureId);
            if (account is not null)
            {
                account.IsDeleted = true;
                _context.Entry(account).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            throw new KeyNotFoundException("AccountNatureId not found.");
        }

        public async Task<List<AccountNature>> GetAllAsync()
        {
            var accounts = await _context.AccountNature.Where(acc => acc.IsDeleted == false).ToListAsync();
            return accounts;
        }

        public async Task<AccountNature> GetAsync(int accountNatureId)
        {
            var account = await _context.AccountNature.FirstOrDefaultAsync(acc => acc.AccountNatureId == accountNatureId && acc.IsDeleted== false);
            if (account is not null)
            {
                return account;
            }
            throw new KeyNotFoundException("AccountNatureId not found");
        }

        public async Task<AccountNature> UpdateAsync(AccountNature accountNature)
        {
            await _context.AccountNature.AddAsync(accountNature);
            _context.Entry(accountNature).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return accountNature;
        }
    }
}
