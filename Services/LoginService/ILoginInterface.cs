using GestVeicular.DTO;
using GestVeicular.Enums;
using GestVeicular.Models;

namespace GestVeicular.Services.LoginService
{
    public interface ILoginInterface
    {
        Task<Response<Usuario>> RegistrarUsuario(UsuarioRegisterDto usuarioRegisterDto, TipoUsuario tipoUsuarioLogado);
        Task<Response<Usuario>> Login(UsuarioLoginDto usuarioLoginDto);
    }
}
