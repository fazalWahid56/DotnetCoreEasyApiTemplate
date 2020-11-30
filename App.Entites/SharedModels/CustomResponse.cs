using System;
using System.Collections.Generic;


namespace App.Entites.SharedModels
{
    public class CustomResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
}
