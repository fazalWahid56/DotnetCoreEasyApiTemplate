using CoreTemplate.App.Db.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreTemplate.App.Db.Repositories
{
    public interface IVoucherRepository
    {
        public Task<List<Voucher>> GetAllAsync();
        public Task<Voucher> GetAsync(int accountId);
        public Task<Voucher> CreateAsync(Voucher accountDTO);
        public Task<Voucher> UpdateAsync(Voucher accountDTO);
        public Task<bool> DeleteAsync(int accountId);
        public Task<List<Voucher>> FindAsync(Expression<Func<Voucher, bool>> predicate);
    }
}
