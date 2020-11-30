using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace App.Entites.SharedModels
{
    public class UserRegistrationModel
    {
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Password { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string ConfirmPassword { get; set; }

        [ProtectedPersonalData]
        public virtual string PhoneNumber { get; set; }

        [ProtectedPersonalData]
        [StringLength(100, MinimumLength = 5)]
        public virtual string UserName { get; set; }

    }
}
