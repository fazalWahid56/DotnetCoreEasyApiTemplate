using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entites.DTO
{
    public class AccountDTO
    {
        public int AccountId { get; set; }
        [Required]
        [MaxLength(200)]
        public string AccountTittle { get; set; }
        [MaxLength(500)]
        public string AccountDescription { get; set; }
        public int AccountNatureId { get; set; }
        public AccountNatureDTO AccountNature { get; set; }
    }

    public class AccountNatureDTO
    {
        public int AccountNatureId { get; set; }
        [Required]
        [MaxLength(200)]
        public string Description { get; set; }
    }
}
