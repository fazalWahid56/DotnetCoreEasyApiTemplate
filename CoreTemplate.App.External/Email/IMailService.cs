using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreTemplate.App.External.Email
{
    public interface IMailService
    {
        Task SendEmailAsync(string toEmail, string subject, string content);
    }
}
