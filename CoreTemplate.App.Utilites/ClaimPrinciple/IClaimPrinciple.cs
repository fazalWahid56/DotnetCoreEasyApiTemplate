using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreTemplate.App.Utilites.ClaimPrinciple
{
    public interface IClaimPrinciple
    {
        UserClaims GetUserClaims();
    }
}
