using CoreTemplate.App.Entites.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreTemplate.App.Services.GeneralLeadger
{
    public interface IVoucherService
    {
        public Task<List<VoucherDTO>> GetAllVouchersAsync();
        public Task<VoucherDTO> GetVoucherAsync(int voucherId);
        public Task<VoucherDTO> CreateVoucherAsync(VoucherDTO voucherDTO);
        public Task<VoucherDTO> UpdateVoucherAsync(VoucherDTO voucherDTO);
        public Task<bool> DeleteVoucherAsync(int voucherId);


        public Task<List<VoucherTypeDTO>> GetAllVoucherTypesAsync();
        public Task<VoucherTypeDTO> GetVoucherTypeAsync(int vouchernatureId);
        public Task<VoucherTypeDTO> CreateVoucherTypeAsync(VoucherTypeDTO voucherTypeDTO);
        public Task<VoucherTypeDTO> UpdateVoucherTypeAsync(VoucherTypeDTO voucherTypeDTO);
        public Task<bool> DeleteVoucherTypeAsync(int vouchernatureId);
    }
}
