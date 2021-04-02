using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Db.Tables
{
    public class ChartOfAccount:BaseEntity
    {
        [Key]
        public int AccountId { get; set; }
        [Required]
        [MaxLength(300)]
        public string AccountTittle { get; set; }
        [MaxLength(500)]
        public string AccountDescription { get; set; }
        [Required]
        [ForeignKey("AccountNatureId")]
        public int AccountNatureId { get; set; }
        public AccountNature AccountNature { get; set; }
        [ForeignKey("FirmId")]
        public int? FirmId { get; set; }
        public Firm Firm { get; set; }

    }
}
