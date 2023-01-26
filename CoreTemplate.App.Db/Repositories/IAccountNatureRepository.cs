using CoreTemplate.App.Db.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreTemplate.App.Db.Repositories
{
    public interface IAccountNatureRepository
    {
        public Task<List<AccountNature>> GetAllAsync();
        public Task<AccountNature> GetAsync(int accountNatureId);
        public Task<AccountNature> CreateAsync(AccountNature accountNature);
        public Task<AccountNature> UpdateAsync(AccountNature accountNature);
        public Task<bool> DeleteAsync(int accountNatureId);
    }
}
