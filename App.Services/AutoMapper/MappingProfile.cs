using App.Db.Tables;
using App.Entites.DTO;
using AutoMapper;

namespace App.Services.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<ChartOfAccount, AccountDTO>();
            CreateMap<AccountDTO, ChartOfAccount>();
            CreateMap<AccountNature, AccountNatureDTO>();
            CreateMap<AccountNatureDTO, AccountNature>();

            CreateMap<VoucherType, VoucherTypeDTO>();
            CreateMap<VoucherTypeDTO, VoucherType>();

            CreateMap<Voucher, VoucherDTO>();
            CreateMap<VoucherDTO, Voucher>();

            CreateMap<GeneralLedger, GeneralLedgerDTO>();
            CreateMap<GeneralLedgerDTO, GeneralLedger>();
        }
    }
}
