using System.ComponentModel.DataAnnotations;

namespace App.Db.Tables
{
    public class Firm:BaseEntity
    {
        [Key]
        public int FirmId { get; set; }
        [Required]
        [MaxLength(300)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
