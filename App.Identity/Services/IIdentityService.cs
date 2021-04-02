using App.Entites.SharedModels;
using App.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

using System.Threading.Tasks;

namespace App.Identity.Services
{
    public interface IIdentityService
    {
        Task<CustomResponse> RegisterUserAsync(UserRegistrationModel model);

        Task<CustomResponse> LoginUserAsync(LoginModel model);

        Task<CustomResponse> ConfirmEmailAsync(string userId, string token);

        Task<CustomResponse> ForgetPasswordAsync(string email);

        Task<CustomResponse> ResetPasswordAsync(ResetPasswordModel model);
        Task<List<ApplicationUser>> GetAllAsync();
    }
}
