using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustokk.BLL.Services.Contracts
{
    public interface IEmailService
    {
        void SendEmail(string toEmail, string subject, string emailBody);
    }
}
