using System.Collections.Generic;
using System.Threading.Tasks;
using CoreTemplate.App.Entites.DTO;
using AutoMapper;
using CoreTemplate.App.Db.Tables;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CoreTemplate.App.Db;
using CoreTemplate.App.Utilites.ClaimPrinciple;

namespace CoreTemplate.App.Services.GeneralLeadger
{
    public class VoucherService : IVoucherService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _db;
        private readonly IClaimPrinciple _user;

        public VoucherService(AppDbContext appDbContext, IClaimPrinciple user, IMapper mapper)
        {
            _mapper = mapper;
            _db = appDbContext;
            _user = user;
        }

        #region Voucher
        public async Task<List<VoucherDTO>> GetAllVouchersAsync()
        {
            var vouchers = await _db.Vouchers.Where(acc => acc.IsDeleted == false).ToListAsync();
            return _mapper.Map<List<VoucherDTO>>(vouchers);
        }
        public async Task<VoucherDTO> GetVoucherAsync(int voucherId)
        {
            var voucher = await _db.Vouchers.FirstOrDefaultAsync(acc => acc.VoucherId == voucherId);
            return _mapper.Map<VoucherDTO>(voucher);
        }
        public async Task<VoucherDTO> CreateVoucherAsync(VoucherDTO voucherDTO)
        {
            voucherDTO.VoucherNo = await GetVoucherNumber("BPV",DateTime.Now);           
            var voucher = _mapper.Map<Voucher>(voucherDTO);
            voucher.CreatedDate = DateTime.Now;
            await _db.Vouchers.AddAsync(voucher);            
            _db.SaveChanges();
            return _mapper.Map<VoucherDTO>(voucher);
        }

        public async Task<VoucherDTO> UpdateVoucherAsync(VoucherDTO voucherDTO)
        {
            var voucher = _mapper.Map<Voucher>(voucherDTO);
            await _db.Vouchers.AddAsync(voucher);
            _db.Entry(voucher).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return voucherDTO;
        }

        public async Task<bool> DeleteVoucherAsync(int voucherId)
        {
            var account = await _db.Vouchers.FirstOrDefaultAsync(acc => acc.VoucherId == voucherId);
            if (account is not null)
            {
                account.IsDeleted = true;
                _db.Entry(account).State = EntityState.Modified;
                _db.SaveChanges();
                return true;
            }
            throw new KeyNotFoundException("VoucherId not found.");
        }

        private async Task<string> GetVoucherNumber(string voucherType, DateTime date)
        {
            string dateMonth = date.ToString("MMM");
            string dateYear = date.Year.ToString();
            string newVoucher = voucherType + "-" + dateYear + "/" + dateMonth+"/";
            //BPV-2021/Feb/001
            //Get last Voucher number
            var vouchers = await _db.Vouchers.Where(a => a.VoucherNo.Contains(dateYear + "/" + dateMonth)).ToListAsync();
            vouchers = vouchers.OrderByDescending(a => a.CreatedDate).ToList();
            if (vouchers is null && vouchers.Any())
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
            var voucherNature = await _db.VoucherTypes.Where(acc => acc.IsDeleted == false).ToListAsync();           
            return _mapper.Map<List<VoucherTypeDTO>>(voucherNature);
        }

        public async Task<VoucherTypeDTO> GetVoucherTypeAsync(int voucherTypeId)
        {
            var voucherType = await _db.VoucherTypes.FirstOrDefaultAsync(acc => acc.VoucherTypeId == voucherTypeId && acc.IsDeleted == false);
            return _mapper.Map<VoucherTypeDTO>(voucherType);

        }

        public async Task<VoucherTypeDTO> CreateVoucherTypeAsync(VoucherTypeDTO voucherTypeDTO)
        {

            var voucherType = _mapper.Map<VoucherType>(voucherTypeDTO);
            var claims = _user.GetUserClaims();
            voucherType.CreatedBy = claims.UserName;
            await _db.VoucherTypes.AddAsync(voucherType);
            await _db.SaveChangesAsync();
            return _mapper.Map<VoucherTypeDTO>(voucherType);
        }

        public async Task<VoucherTypeDTO> UpdateVoucherTypeAsync(VoucherTypeDTO voucherTypeDTO)
        {
            var voucherType = _mapper.Map<VoucherType>(voucherTypeDTO);
            await _db.VoucherTypes.AddAsync(voucherType);
            _db.Entry(voucherType).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return voucherTypeDTO;
        }

        public async Task<bool> DeleteVoucherTypeAsync(int voucherTypeId)
        {
            var account = await _db.VoucherTypes.FirstOrDefaultAsync(acc => acc.VoucherTypeId == voucherTypeId);
            if (account is not null)
            {
                account.IsDeleted = true;
                _db.Entry(account).State = EntityState.Modified;
                _db.SaveChanges();
                return true;
            }
            throw new KeyNotFoundException("VoucherTypeId not found.");
        }
        
        #endregion
    }
}
