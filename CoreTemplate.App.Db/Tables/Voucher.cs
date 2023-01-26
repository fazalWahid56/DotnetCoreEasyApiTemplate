using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CoreTemplate.App.Db.Tables
{
    public class Voucher : BaseEntity
    {
        [Key]
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
        [ForeignKey("VoucherTypeId")]
        public int VoucherTypeId { get; set; }
        [ForeignKey("FirmId")]
        public int? FirmId { get; set; }
        public Firm Firm { get; set; }
        public VoucherType VoucherType { get; set; }
        public List<ChartOfAccount> ChartOfAccount { get; set; }
    }

    public class VoucherType : BaseEntity
    {
        [Key]
        public int VoucherTypeId { get; set; }
        [MaxLength(200)]
        [Required]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Desciption { get; set; }
        public List<Voucher> Voucher { get; set; }
    }

}

