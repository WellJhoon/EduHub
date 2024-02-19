using static ProyectoFinal.DTOs.UsuarioDTO;

namespace ProyectoFinal.Interfaces
{
    public interface IUserService
    {
        Task RegisterUser(RegisterUserRequestDto usuario);

        Task<(bool, string)> LoginUser(LoginUserRequestDto request);


    }
}
