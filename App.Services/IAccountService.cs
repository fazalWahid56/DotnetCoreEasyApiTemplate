using App.Entites.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services
{
    public interface IAccountService
    {
        public Task<List<AccountDTO>> GetAllAccountsAsync();
        public Task<AccountDTO> GetAccountAsync(int accountId);
        public Task<AccountDTO> CreateAccountAsync(AccountDTO accountDTO);
        public Task<AccountDTO> UpdateAccountAsync(AccountDTO accountDTO);
        public Task<bool> DeleteAccountAsync(int accountId);


        public Task<List<AccountNatureDTO>> GetAllAccountNaturesAsync();
        public Task<AccountNatureDTO> GetAccountNatureAsync(int accountnatureId);
        public Task<AccountNatureDTO> CreateAccountNatureAsync(AccountNatureDTO accountNatureDTO);
        public Task<AccountNatureDTO> UpdateAccountNatureAsync(AccountNatureDTO accountNatureDTO);
        public Task<bool> DeleteAccountNatureAsync(int accountnatureId);
    }
}
