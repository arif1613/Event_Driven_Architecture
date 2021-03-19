using System.Threading.Tasks;

namespace OrderService.Api.Utils.EmailSender
{
    public interface IEmailSender
    {
        Task SendEmail(string HtmlEmailData);
    }
}