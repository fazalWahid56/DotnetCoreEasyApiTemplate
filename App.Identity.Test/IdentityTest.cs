using App.Entites.SharedModels;
using App.External.Email;
using App.Identity.Services;
using App.Utilites.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using FluentAssertions;
using System;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;

namespace App.Identity.Test
{
    [TestClass]
    public class IdentityTest
    {
        private Mock<UserManager<IdentityUser>> userManagerService { get; set; }
        private Mock<IConfigurationSection> mockConfSection { get; set; }
        private Mock<IMailService> mockEmailSerivce { get; set; }
        private IdentityUser _user;
        string _password;
        string _EmailVarificationToken;
        [TestInitialize]
        public void SetUp()
        {
            //Mock User 
            _user = new IdentityUser { Email = "test@email.com", Id = "TestUserId" };
            _password = "test123";
            _EmailVarificationToken = "TestToken";
            //Mock configurations 
            mockConfSection = new Mock<IConfigurationSection>();
            mockConfSection.SetupGet(m => m[It.Is<string>(s => s == "AppUrl")]).Returns("Http://test.com");
            mockConfSection.SetupGet(m => m[It.Is<string>(s => s == "AuthSettings:Key")]).Returns("This is the key that we will use in the encryption");
            mockConfSection.SetupGet(m => m[It.Is<string>(s => s == "AuthSettings:Issuer")]).Returns("http://GenApps.net");
            mockConfSection.SetupGet(m => m[It.Is<string>(s => s == "AuthSettings:Audience")]).Returns("http://GenApps.net");

            //Mock Email Service
            mockEmailSerivce = new Mock<IMailService>();

            //Mock User service
            var userStoreMock = new Mock<IUserStore<IdentityUser>>();
            userManagerService = new Mock<UserManager<IdentityUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            
            userManagerService.Setup(user => user.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                 .ReturnsAsync(IdentityResult.Success);
            
            userManagerService.Setup(user => user.GenerateEmailConfirmationTokenAsync(It.IsAny<IdentityUser>()))
                 .Returns(Task.FromResult("testToken"));
            
            userManagerService.Setup(email => email.FindByEmailAsync(It.IsAny<string>()))
                 .ReturnsAsync((string param) => param.Equals(_user.Email)?_user:null);
            
            userManagerService.Setup(pass => pass.CheckPasswordAsync(It.IsAny<IdentityUser>(), It.IsAny<string>() ))
                 .ReturnsAsync( (IdentityUser usr, string pass ) => pass.Equals(_password)?true:false );
            
            userManagerService.Setup(id => id.FindByIdAsync(It.IsAny<string>()))
                 .ReturnsAsync((string param) => param.Equals(_user.Id)? _user : null);
            
            userManagerService.Setup(pass => pass.ConfirmEmailAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                 .ReturnsAsync((IdentityUser usr, string token) => token.Equals(_EmailVarificationToken)  ? IdentityResult.Success : IdentityResult.Failed());
            
            userManagerService.Setup(user => user.GeneratePasswordResetTokenAsync(It.IsAny<IdentityUser>()))
            .Returns(Task.FromResult(_EmailVarificationToken));

            userManagerService.Setup(pass => pass.ResetPasswordAsync(It.IsAny<IdentityUser>(), It.IsAny<string>(), It.IsAny<string>()))
                 .ReturnsAsync((IdentityUser usr, string token, string newPass) => token.Equals(_EmailVarificationToken) ? IdentityResult.Success : IdentityResult.Failed());
            
        }

        [TestMethod]
        public async Task RegisterUserAsync()
        {
            IIdentityService identityService = new IdentityService(userManagerService.Object, mockConfSection.Object, mockEmailSerivce.Object);
            // Act
            var result = await identityService.RegisterUserAsync(new UserRegistrationModel() { Email =_user.Email, Password = _password, ConfirmPassword = "test123" });
            // Assert
            result.IsSuccess.Should().BeTrue();
        }
        [TestMethod]
        [ExpectedException(typeof(HttpStatusException), "Confirm password doesn't match the password")]
        public async Task RegisterUserAsyncNotSamePass()
        {
            IIdentityService identityService = new IdentityService(userManagerService.Object, mockConfSection.Object, mockEmailSerivce.Object);
            // Act
            var result = await identityService.RegisterUserAsync(new UserRegistrationModel() { Email = _user.Email, Password = _password, ConfirmPassword = "123test" });
            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(HttpStatusException), "Reigster Model is null")]
        public async Task RegisterUserAsyncNullModel()
        {
            IIdentityService identityService = new IdentityService(userManagerService.Object, mockConfSection.Object, mockEmailSerivce.Object);
            // Act
            var result = await identityService.RegisterUserAsync(new UserRegistrationModel() { Email = _user.Email, Password = _password, ConfirmPassword = "123test" });
            // Assert
        }

        [TestMethod]
        public async Task LoginUserAsync()
        {
            IIdentityService identityService = new IdentityService(userManagerService.Object, mockConfSection.Object, mockEmailSerivce.Object);
            // Act
            var result = await identityService.LoginUserAsync(new LoginModel() { Email = _user.Email, Password = _password });
            // Assert
            result.IsSuccess.Should().BeTrue();
        }
        [TestMethod]
        [ExpectedException(typeof(HttpStatusException), "User name is incorrect")]
        public async Task LoginUserAsyncWrongEmail()
        {
            IIdentityService identityService = new IdentityService(userManagerService.Object, mockConfSection.Object, mockEmailSerivce.Object);
            // Act
           await identityService.LoginUserAsync(new LoginModel() { Email = "Wrong@email.com", Password = _password });
            
        }

        [TestMethod]
        [ExpectedException(typeof(HttpStatusException), "User password is incorrect")]
        public async Task LoginUserAsyncWrongPass()
        {
            IIdentityService identityService = new IdentityService(userManagerService.Object, mockConfSection.Object, mockEmailSerivce.Object);
            // Act
            await identityService.LoginUserAsync(new LoginModel() { Email = _user.Email, Password = "WrongPassword" });           
        }


        [TestMethod]
        public async Task ConfirmEmailAsync()
        {
            IIdentityService identityService = new IdentityService(userManagerService.Object, mockConfSection.Object, mockEmailSerivce.Object);
            // Act
            var encodedToken = Encoding.UTF8.GetBytes(_EmailVarificationToken);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);
            var result = await identityService.ConfirmEmailAsync(_user.Id,WebEncoders.Base64UrlEncode(encodedToken));
            // Assert
            result.IsSuccess.Should().BeTrue();
        }
        [TestMethod]
        [ExpectedException(typeof(HttpStatusException), "User name is incorrect")]
        public async Task ConfirmEmailUserNotFound()
        {
            IIdentityService identityService = new IdentityService(userManagerService.Object, mockConfSection.Object, mockEmailSerivce.Object);
            // Act
            await identityService.ConfirmEmailAsync("wrong userID","stringToken");

        }

        [TestMethod]
        [ExpectedException(typeof(HttpStatusException), "Email did not confirm")]
        public async Task ConfirmEmailWrongToken()
        {

            var encodedToken = Encoding.UTF8.GetBytes("wrongToken");
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);
            IIdentityService identityService = new IdentityService(userManagerService.Object, mockConfSection.Object, mockEmailSerivce.Object);
            // Act
            await identityService.ConfirmEmailAsync(_user.Id, validToken);
        }

        [TestMethod]
        public async Task ForgetPasswordAsync()
        {
            IIdentityService identityService = new IdentityService(userManagerService.Object, mockConfSection.Object, mockEmailSerivce.Object);
            // Act
            var result = await identityService.ForgetPasswordAsync(_user.Email);
            // Assert
            result.IsSuccess.Should().BeTrue();
        }
        [TestMethod]
        [ExpectedException(typeof(HttpStatusException), "No user associated with email")]
        public async Task ForgetPassUserWithEmailNotFound()
        {
            IIdentityService identityService = new IdentityService(userManagerService.Object, mockConfSection.Object, mockEmailSerivce.Object);
            // Act
            await identityService.ForgetPasswordAsync("WrongEmail@mail.com");

        }

        [TestMethod]
        public async Task ResetPasswordAsync()
        {
            IIdentityService identityService = new IdentityService(userManagerService.Object, mockConfSection.Object, mockEmailSerivce.Object);
            // Act
            var encodedToken = Encoding.UTF8.GetBytes(_EmailVarificationToken);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);
            var result = await identityService.ResetPasswordAsync(new ResetPasswordModel { Email=_user.Email, NewPassword=_password, ConfirmPassword=_password, Token=validToken });
            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [TestMethod]
        [ExpectedException(typeof(HttpStatusException), "No user associated with email")]
        public async Task ResetPasswordAsyncEmailNotFound()
        {
            IIdentityService identityService = new IdentityService(userManagerService.Object, mockConfSection.Object, mockEmailSerivce.Object);
            // Act
            var encodedToken = Encoding.UTF8.GetBytes(_EmailVarificationToken);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);
            await identityService.ResetPasswordAsync(new ResetPasswordModel { Email = "WrongEmail@mail.com", NewPassword = _password, ConfirmPassword = _password, Token = validToken });

        }

        [TestMethod]
        [ExpectedException(typeof(HttpStatusException), "Password doesn't match its confirmation")]
        public async Task ResetPasswordAsyncPassNotMatched()
        {
            IIdentityService identityService = new IdentityService(userManagerService.Object, mockConfSection.Object, mockEmailSerivce.Object);
            // Act
            var encodedToken = Encoding.UTF8.GetBytes(_EmailVarificationToken);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);
            await identityService.ResetPasswordAsync(new ResetPasswordModel { Email = _user.Email, NewPassword = _password, ConfirmPassword = "WrongPassword", Token = validToken });

        }

        [TestMethod]
        [ExpectedException(typeof(HttpStatusException), "Token not valid")]
        public async Task ResetPasswordAsyncTokenNotValid()
        {
            IIdentityService identityService = new IdentityService(userManagerService.Object, mockConfSection.Object, mockEmailSerivce.Object);
            // Act
            var encodedToken = Encoding.UTF8.GetBytes("SomeWrongToken");
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);
            await identityService.ResetPasswordAsync(new ResetPasswordModel { Email = _user.Email, NewPassword = _password, ConfirmPassword = "WrongPassword", Token = validToken });

        }


    }
}
