using App.Db.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Db.Repositories
{
    public interface IChartOfAccountRepository
    {
        public Task<List<ChartOfAccount>> GetAllAsync();
        public Task<ChartOfAccount> GetAsync(int accountId);
        public Task<ChartOfAccount> CreateAsync(ChartOfAccount accountDTO);
        public Task<ChartOfAccount> UpdateAsync(ChartOfAccount accountDTO);
        public Task<bool> DeleteAsync(int accountId);
    }
}
