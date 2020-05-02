using System.Threading.Tasks;

namespace HelloSocNetw_BLL.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }

}
