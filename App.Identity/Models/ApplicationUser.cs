using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Identity.Models
{
    public class ApplicationUser: IdentityUser
    {
        public int? FirmId { get; set; }
    }
}
