using System.Collections.Generic;
using System.Threading.Tasks;
using App.Entites.DTO;
using AutoMapper;
using App.Db.Tables;
using App.Db.Repositories;
using System;
using System.Linq;

namespace App.Services.GeneralLeadger
{
    public class VoucherService : IVoucherService
    {
        private readonly IMapper _mapper;
        private readonly IVoucherRepository _voucherRepo;
        private readonly IVoucherTypeRepository _vouchTypeRepo;

        public VoucherService(IVoucherRepository voucherRepo, IVoucherTypeRepository vouchTypeRepo, IMapper mapper)
        {
            _mapper = mapper;
            _voucherRepo = voucherRepo;
            _vouchTypeRepo = vouchTypeRepo;
        }

        #region Voucher
        public async Task<List<VoucherDTO>> GetAllVouchersAsync()
        {
            var vouchers = await _voucherRepo.GetAllAsync();
            return _mapper.Map<List<VoucherDTO>>(vouchers);
        }
        public async Task<VoucherDTO> GetVoucherAsync(int voucherId)
        {
            var voucher = await _voucherRepo.GetAsync(voucherId);
            return _mapper.Map<VoucherDTO>(voucher);
        }
        public async Task<VoucherDTO> CreateVoucherAsync(VoucherDTO voucherDTO)
        {
            voucherDTO.VoucherNo = await GetVoucherNumber("BPV",DateTime.Now);
            var voucher = _mapper.Map<Voucher>(voucherDTO);
            await _voucherRepo.CreateAsync(voucher);
            return _mapper.Map<VoucherDTO>(voucher);
        }

        public async Task<VoucherDTO> UpdateVoucherAsync(VoucherDTO voucherDTO)
        {
            var voucher = _mapper.Map<Voucher>(voucherDTO);
            await _voucherRepo.UpdateAsync(voucher);
            return voucherDTO;
        }

        public async Task<bool> DeleteVoucherAsync(int voucherId)
        {
            return await _voucherRepo.DeleteAsync(voucherId);
        }

        private async Task<string> GetVoucherNumber(string voucherType, DateTime date)
        {
            string dateMonth = date.ToString("MMM");
            string dateYear = date.Year.ToString();
            string newVoucher = voucherType + "-" + dateYear + "/" + dateMonth+"/";
            //BPV-2021/Feb/001

            //Get last Voucher number
            var vouchers = await _voucherRepo.FindAsync(a=>a.VoucherNo.Contains(dateYear+"/"+dateMonth));
          //  vouchers = vouchers.OrderByDescending(a => a.CreatedDate).ToList();
            if (vouchers.Any())
            {
                string preVoucherNo = vouchers.OrderByDescending(a => a.CreatedDate).ToList().FirstOrDefault().VoucherNo;
                int count = Convert.ToInt32(preVoucherNo.Split('/')[2]);
                count = ++count;
                return newVoucher + count;
            }
            return  newVoucher + "1";
        }
        #endregion



        #region VoucherType

        public async Task<List<VoucherTypeDTO>> GetAllVoucherTypesAsync()
        {
            var voucherNature = await _vouchTypeRepo.GetAllAsync();
            return _mapper.Map<List<VoucherTypeDTO>>(voucherNature);
        }

        public async Task<VoucherTypeDTO> GetVoucherTypeAsync(int vouchernatureId)
        {
            var voucherNature = await _vouchTypeRepo.GetAsync(vouchernatureId);
            return _mapper.Map<VoucherTypeDTO>(voucherNature);

        }

        public async Task<VoucherTypeDTO> CreateVoucherTypeAsync(VoucherTypeDTO voucherNatureDTO)
        {
            var voucherNature = _mapper.Map<VoucherType>(voucherNatureDTO);
            await _vouchTypeRepo.CreateAsync(voucherNature);
            return _mapper.Map<VoucherTypeDTO>(voucherNature);
        }

        public async Task<VoucherTypeDTO> UpdateVoucherTypeAsync(VoucherTypeDTO voucherNatureDTO)
        {
            var voucherNature = _mapper.Map<VoucherType>(voucherNatureDTO);
            await _vouchTypeRepo.UpdateAsync(voucherNature);
            return voucherNatureDTO;
        }

        public async Task<bool> DeleteVoucherTypeAsync(int voucherNatureId)
        {
            return await _vouchTypeRepo.DeleteAsync(voucherNatureId);
        }
        
        #endregion
    }
}
