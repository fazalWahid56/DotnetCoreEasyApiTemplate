using CoreTemplate.App.Db.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreTemplate.App.Db.Repositories
{
    public interface IVoucherTypeRepository
    {
        public Task<List<VoucherType>> GetAllAsync();
        public Task<VoucherType> GetAsync(int voucherTypeId);
        public Task<VoucherType> CreateAsync(VoucherType accountDTO);
        public Task<VoucherType> UpdateAsync(VoucherType accountDTO);
        public Task<bool> DeleteAsync(int voucherTypeId);
    }
}
