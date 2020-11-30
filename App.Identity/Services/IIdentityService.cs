using App.Entites.SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
