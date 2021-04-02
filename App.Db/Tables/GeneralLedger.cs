using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Db.Tables
{
    public class GeneralLedger : BaseEntity
    {
        [Key]
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
        [ForeignKey("AgainstAccountId")]
        public int? AgainstAccountId { get; set; }
        [ForeignKey("FromAccountId")]
        public int? FromAccountId { get; set; }
        [Required]
        [ForeignKey("VoucherId")]
        public int VoucherId { get; set; }
        [ForeignKey("FirmId")]
        public int? FirmId { get; set; }
        public Firm Firm { get; set; }
        public Voucher Voucher { get; set; }
        public ChartOfAccount AgainstAccount { get; set; }
        public ChartOfAccount FromAccount { get; set; }
    }

}