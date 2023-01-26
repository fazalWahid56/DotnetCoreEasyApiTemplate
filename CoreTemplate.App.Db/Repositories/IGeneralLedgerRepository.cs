using CoreTemplate.App.Db.Tables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreTemplate.App.Db.Repositories
{
    public interface IGeneralLedgerRepository
    {
        public Task<List<GeneralLedger>> GetAllAsync();
        public Task<GeneralLedger> GetAsync(int transectionId);
        public Task<GeneralLedger> CreateAsync(GeneralLedger GeneralLedger);
        public Task<GeneralLedger> UpdateAsync(GeneralLedger GeneralLedger);
        public Task<bool> DeleteAsync(int transectionId);
    }
}
