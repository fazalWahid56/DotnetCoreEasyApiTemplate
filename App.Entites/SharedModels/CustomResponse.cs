using System;
using System.Collections.Generic;


namespace App.Entites.SharedModels
{
    public class CustomResponse
    {
        public string token { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string username {get; set;}
        public string email {get; set;}
        public string picture {get; set;}
        public string password {get; set;}
        public string firstName {get; set;}
        public string lastName {get; set;}
    }
}
