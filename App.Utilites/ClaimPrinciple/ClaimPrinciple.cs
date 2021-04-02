using System;
using System.Security.Claims;

namespace App.Utilites.ClaimPrinciple
{
    public class ClaimPrinciple : IClaimPrinciple
    {
        private ClaimsPrincipal _user;
        public ClaimPrinciple(ClaimsPrincipal user) {
            _user = user;
        }
        public UserClaims GetUserClaims()
        {
            UserClaims uc = new UserClaims();
            uc.FirmId= Convert.ToInt32(_user.FindFirst("FirmId").Value);
            uc.Email = _user.FindFirst("Email").Value;
            uc.UserName = _user.FindFirst("UserName").Value;
            uc.UserId = _user.FindFirst("UserId").Value;
            return uc;
        }
    }
}
