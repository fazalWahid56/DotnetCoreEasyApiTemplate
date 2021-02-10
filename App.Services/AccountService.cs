using System.Collections.Generic;
using System.Threading.Tasks;
using App.Entites.DTO;
using AutoMapper;
using App.Db.Tables;
using App.Db.Repositories;

namespace App.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IChartOfAccountRepository _accountRepo;
        private readonly IAccountNatureRepository _accNatureRepo;

        public AccountService(IChartOfAccountRepository accountRepo, IAccountNatureRepository accNatureRepo, IMapper mapper)
        {
            _mapper = mapper;
            _accountRepo = accountRepo;
            _accNatureRepo = accNatureRepo;
        }

        #region Account
        public async Task<List<AccountDTO>> GetAllAccountsAsync()
        {
            var accounts = await _accountRepo.GetAllAsync();
            return _mapper.Map<List<AccountDTO>>(accounts);
        }
        public async Task<AccountDTO> GetAccountAsync(int accountId)
        {
            var account = await _accountRepo.GetAsync(accountId);
            return _mapper.Map<AccountDTO>(account);
        }
        public async Task<AccountDTO> CreateAccountAsync(AccountDTO accountDTO)
        {
            var account = _mapper.Map<ChartOfAccount>(accountDTO);
            await _accountRepo.CreateAsync(account);
            return _mapper.Map<AccountDTO>(account);
        }

        public async Task<AccountDTO> UpdateAccountAsync(AccountDTO accountDTO)
        {
            var account = _mapper.Map<ChartOfAccount>(accountDTO);
            await _accountRepo.UpdateAsync(account);
            return accountDTO;
        }

        public async Task<bool> DeleteAccountAsync(int accountId)
        {
            return await _accountRepo.DeleteAsync(accountId);
        }
        #endregion



        #region Account Nature

        public async Task<List<AccountNatureDTO>> GetAllAccountNaturesAsync()
        {
            var accountNature = await _accNatureRepo.GetAllAsync();
            return _mapper.Map<List<AccountNatureDTO>>(accountNature);
        }

        public async Task<AccountNatureDTO> GetAccountNatureAsync(int accountnatureId)
        {
            var accountNature = await _accNatureRepo.GetAsync(accountnatureId);
            return _mapper.Map<AccountNatureDTO>(accountNature);

        }

        public async Task<AccountNatureDTO> CreateAccountNatureAsync(AccountNatureDTO accountNatureDTO)
        {
            var accountNature = _mapper.Map<AccountNature>(accountNatureDTO);
            await _accNatureRepo.CreateAsync(accountNature);
            return _mapper.Map<AccountNatureDTO>(accountNature);
        }

        public async Task<AccountNatureDTO> UpdateAccountNatureAsync(AccountNatureDTO accountNatureDTO)
        {
            var accountNature = _mapper.Map<AccountNature>(accountNatureDTO);
            await _accNatureRepo.UpdateAsync(accountNature);
            return accountNatureDTO;
        }

        public async Task<bool> DeleteAccountNatureAsync(int accountNatureId)
        {
            return await _accNatureRepo.DeleteAsync(accountNatureId);
        }
        #endregion
    }
}
