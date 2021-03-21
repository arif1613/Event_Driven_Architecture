using System.Threading;
using System.Threading.Tasks;

namespace OrderService.Api.Utils.EmailSender
{
    public class EmailSender:IEmailSender
    {
        public Task SendEmail(string HtmlEmailData)
        {
            //Email will be sent using SMTP
           return Task.Run(() =>Thread.Sleep(2000));
        }
    }
}
