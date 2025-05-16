using GestVeicular.Data;
using GestVeicular.DTO;
using GestVeicular.Models;

namespace GestVeicular.Services.LoginService
{
    public class LoginService : ILoginInterface
    {
        private readonly ApplicationDbContext _context;
        
        public LoginService(ApplicationDbContext context)
        {
            _context = context;
        }
    public Task<Response<Usuario>> Login(UsuarioLoginDto usuarioLoginDto)
        {
            
        }

        public Task<Response<Usuario>> RegistrarUsuario(UsuarioRegisterDto usuarioRegisterDto)
        {
            throw new NotImplementedException();
        }
    }
}
