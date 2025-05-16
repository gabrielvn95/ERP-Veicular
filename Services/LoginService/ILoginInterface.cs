using GestVeicular.DTO;
using GestVeicular.Models;

namespace GestVeicular.Services.LoginService
{
    public interface ILoginInterface
    {
        Task<Response<Usuario>> RegistrarUsuario(UsuarioRegisterDto usuarioRegisterDto);
        Task<Response<Usuario>> Login(UsuarioLoginDto usuarioLoginDto);
    }
}
