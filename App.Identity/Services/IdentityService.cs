using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.External.Email;
using App.Entites.SharedModels;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using App.Utilites.Exceptions;
using Newtonsoft.Json;

namespace App.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private UserManager<IdentityUser> _userManger;
        private IConfiguration _configuration;
        private IMailService _mailService;
        public IdentityService(UserManager<IdentityUser> userManager, IConfiguration configuration, IMailService mailService)
        {
            _userManger = userManager;
            _configuration = configuration;
            _mailService = mailService;
        }

        public async Task<CustomResponse> RegisterUserAsync(UserRegistrationModel model)
        {
            if (model == null)
                throw new HttpStatusException(System.Net.HttpStatusCode.BadRequest, "Reigster Model is null");

            if (model.Password != model.ConfirmPassword)
                throw new HttpStatusException(System.Net.HttpStatusCode.BadRequest, "Confirm password doesn't match the password");


            var identityUser = new IdentityUser
            {
                Email = model.Email,
                UserName = model.Email,
            };

            var result = await _userManger.CreateAsync(identityUser, model.Password);

            if (result.Succeeded)
            {
                var confirmEmailToken = await _userManger.GenerateEmailConfirmationTokenAsync(identityUser);

                var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
                var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

                string url = $"{_configuration["AppUrl"]}/api/user/confirmemail?userid={identityUser.Id}&token={validEmailToken}";

                await _mailService.SendEmailAsync(identityUser.Email, "Confirm your email", $"<h1>Welcome to Auth Demo</h1>" +
                    $"<p>Please confirm your email by <a href='{url}'>Clicking here</a></p>");

                return new CustomResponse
                {
                    Message = "User created successfully!",
                    IsSuccess = true,
                };
            }

            throw new HttpStatusException(System.Net.HttpStatusCode.BadRequest, JsonConvert.SerializeObject(result.Errors));

        }

        public async Task<CustomResponse> LoginUserAsync(LoginModel model)
        {

            var user = await _userManger.FindByEmailAsync(model.Email);

            if (user == null)
            {
                throw new HttpStatusException(System.Net.HttpStatusCode.BadRequest, "User name or password is incorrect");
            }

            var result = await _userManger.CheckPasswordAsync(user, model.Password);

            if (!result)
                throw new HttpStatusException(System.Net.HttpStatusCode.BadRequest, "User name or password is incorrect");

            var claims = new[]
            {
                new Claim("Email",  Convert.ToString(model.Email)),
                new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.Id)),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["AuthSettings:Issuer"],
                audience: _configuration["AuthSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return new CustomResponse
            {
                Message = tokenAsString,
                IsSuccess = true,
                ExpireDate = token.ValidTo
            };


        }

        public async Task<CustomResponse> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManger.FindByIdAsync(userId);
            if (user == null)
                throw new HttpStatusException(System.Net.HttpStatusCode.NotFound, "User not found");

            var decodedToken = WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManger.ConfirmEmailAsync(user, normalToken);

            if (result.Succeeded)
                return new CustomResponse
                {
                    Message = "Email confirmed successfully!",
                    IsSuccess = true,
                };

            throw new HttpStatusException(System.Net.HttpStatusCode.NotFound, "Email did not confirm");
        }

        public async Task<CustomResponse> ForgetPasswordAsync(string email)
        {
            var user = await _userManger.FindByEmailAsync(email);
            if (user == null)
                throw new HttpStatusException(System.Net.HttpStatusCode.BadRequest, "No user associated with email");

            var token = await _userManger.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);

            string url = $"{_configuration["AppUrl"]}/ResetPassword?email={email}&token={validToken}";

            await _mailService.SendEmailAsync(email, "Reset Password", "<h1>Follow the instructions to reset your password</h1>" +
                $"<p>To reset your password <a href='{url}'>Click here</a></p>");

            return new CustomResponse
            {
                IsSuccess = true,
                Message = "Reset password URL has been sent to the email successfully!"
            };
        }

        public async Task<CustomResponse> ResetPasswordAsync(ResetPasswordModel model)
        {
            var user = await _userManger.FindByEmailAsync(model.Email);
            if (user == null)
                throw new HttpStatusException(System.Net.HttpStatusCode.BadRequest, "No user associated with email");

            if (model.NewPassword != model.ConfirmPassword)
                throw new HttpStatusException(System.Net.HttpStatusCode.BadRequest, "Password doesn't match its confirmation");

            var decodedToken = WebEncoders.Base64UrlDecode(model.Token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManger.ResetPasswordAsync(user, normalToken, model.NewPassword);

            if (result.Succeeded)
                return new CustomResponse
                {
                    Message = "Password has been reset successfully!",
                    IsSuccess = true,
                };

            throw new HttpStatusException(System.Net.HttpStatusCode.BadRequest, JsonConvert.SerializeObject(result.Errors));
        }
    }
}

