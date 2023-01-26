using System;
using System.ComponentModel.DataAnnotations;

namespace CoreTemplate.App.Entites.DTO
{
    public class GeneralLedgerDTO
    {
        public int TransectionId { get; set; }
        [Required]
        [MaxLength(200)]
        public string ReferenceNo { get; set; }
        [MaxLength(200)]
        public string ChequeNo { get; set; }
        [MaxLength(500)]
        public string Narration { get; set; }
        public decimal DrAmmount { get; set; }
        public decimal CrAmmount { get; set; }     
        public int? AgainstAccountId { get; set; }
        public int? FromAccountId { get; set; }
        [Required]
        public int VoucherId { get; set; }
        public VoucherDTO Voucher { get; set; }
        public AccountDTO AgainstAccount { get; set; }
        public AccountDTO FromAccount { get; set; }
        public int? FirmId { get; set; }
        public FirmDTO Firm { get; set; }
    }
    public class VoucherDTO
    {
        public int VoucherId { get; set; }
        [Required]
        [MaxLength(200)]
        public string VoucherNo { get; set; }
        public DateTime Date { get; set; }
        [MaxLength(500)]
        public string Desciption { get; set; }
        public decimal Ammount { get; set; }
        [MaxLength(100)]
        public string Month { get; set; }
        [MaxLength(100)]
        public string Year { get; set; }
        [Required]
        public int VoucherTypeId { get; set; }
        public VoucherTypeDTO VoucherType { get; set; }
        public int? FirmId { get; set; }
        public FirmDTO Firm { get; set; }
    }

    public class VoucherTypeDTO
    {
        public int VoucherTypeId { get; set; }
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Desciption { get; set; }
    }
}
