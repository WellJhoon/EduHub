using ProyectoFinal.Models;

namespace ProyectoFinal.Services.Email
{
    public interface IEmail
    {
        void SendEmail(EmailDto request);

    }
}
