using System.Collections.Generic;
using System.Threading.Tasks;
using App.Entites.DTO;
using AutoMapper;
using App.Db.Tables;
using App.Db;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace App.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _db;

        public AccountService(AppDbContext _appDbContext, IMapper mapper)
        {
            _mapper = mapper;
            _db = _appDbContext;
        }

        #region Account
        public async Task<List<AccountDTO>> GetAllAccountsAsync()
        {
            var accounts = await _db.ChartOfAccounts.Where(acc => acc.IsDeleted == false).ToListAsync();
            return _mapper.Map<List<AccountDTO>>(accounts);
        }
        public async Task<AccountDTO> GetAccountAsync(int accountId)
        {
            var account = await _db.ChartOfAccounts.FirstOrDefaultAsync(acc => acc.IsDeleted == false);
            return _mapper.Map<AccountDTO>(account);
        }
        public async Task<AccountDTO> CreateAccountAsync(AccountDTO accountDTO)
        {
            var account = _mapper.Map<ChartOfAccount>(accountDTO);
            await _db.ChartOfAccounts.AddAsync(account);
            await _db.SaveChangesAsync();
            return _mapper.Map<AccountDTO>(account);
        }

        public async Task<AccountDTO> UpdateAccountAsync(AccountDTO accountDTO)
        {
            var account = _mapper.Map<ChartOfAccount>(accountDTO);
            await _db.ChartOfAccounts.AddAsync(account);
            _db.Entry(account).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return accountDTO;
        }

        public async Task<bool> DeleteAccountAsync(int accountId)
        {
            var account = await _db.ChartOfAccounts.FirstOrDefaultAsync(acc => acc.AccountId == accountId);
            if (account is not null)
            {
                _db.Entry(account).State = EntityState.Modified;
                _db.SaveChanges();
                return true;
            }
            throw new KeyNotFoundException("AccountId not found.");
        }
        #endregion



        #region Account Nature

        public async Task<List<AccountNatureDTO>> GetAllAccountNaturesAsync()
        {
            var accountNature = await _db.AccountNature.Where(acc => acc.IsDeleted == false).ToListAsync();
            return _mapper.Map<List<AccountNatureDTO>>(accountNature);
        }

        public async Task<AccountNatureDTO> GetAccountNatureAsync(int accountnatureId)
        {
            var accountNature = await _db.AccountNature.FirstOrDefaultAsync(acc => acc.IsDeleted == false);
            return _mapper.Map<AccountNatureDTO>(accountNature);

        }

        public async Task<AccountNatureDTO> CreateAccountNatureAsync(AccountNatureDTO accountNatureDTO)
        {
            var accountNature = _mapper.Map<AccountNature>(accountNatureDTO);
            await _db.AccountNature.AddAsync(accountNature);
            await _db.SaveChangesAsync();
            return _mapper.Map<AccountNatureDTO>(accountNature);
        }

        public async Task<AccountNatureDTO> UpdateAccountNatureAsync(AccountNatureDTO accountNatureDTO)
        {
            var accountNature = _mapper.Map<AccountNature>(accountNatureDTO);
            await _db.AccountNature.AddAsync(accountNature);
            _db.Entry(accountNature).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return accountNatureDTO;
        }

        public async Task<bool> DeleteAccountNatureAsync(int accountNatureId)
        {
            var account = await _db.AccountNature.FirstOrDefaultAsync(acc => acc.AccountNatureId == accountNatureId);
            if (account is not null)
            {
                account.IsDeleted = true;
                _db.Entry(account).State = EntityState.Modified;
                _db.SaveChanges();
                return true;
            }
            throw new KeyNotFoundException("AccountNatureId not found.");
        }
        #endregion
    }
}
