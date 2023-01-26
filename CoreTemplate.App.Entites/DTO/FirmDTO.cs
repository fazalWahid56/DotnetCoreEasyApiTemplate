using System.ComponentModel.DataAnnotations;

namespace CoreTemplate.App.Entites.DTO
{
    public class FirmDTO
    {
        public int FirmId { get; set; }
        [Required]
        [MaxLength(300)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
