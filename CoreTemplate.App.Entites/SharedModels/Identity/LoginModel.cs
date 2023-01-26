using System.ComponentModel.DataAnnotations;

namespace CoreTemplate.App.Entites.SharedModels
{
    public class LoginModel
    {

        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Password { get; set; }

    }
}
