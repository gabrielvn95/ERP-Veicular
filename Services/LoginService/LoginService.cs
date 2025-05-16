using GestVeicular.Data;
using GestVeicular.DTO;
using GestVeicular.Models;
using GestVeicular.Services.SenhaService;
using GestVeicular.Services.SessaoService;
using Microsoft.EntityFrameworkCore;

namespace GestVeicular.Services.LoginService
{
    public class LoginService : ILoginInterface
    {
        private readonly ApplicationDbContext _context;
        private readonly ISessaoInterface _sessaoInterface;
        private readonly ISenhaInterface _senhaInterface;

        public LoginService(ApplicationDbContext context, ISenhaInterface senhaInterface, ISessaoInterface sessaoInterface)
        {
            _context = context;
            _senhaInterface = senhaInterface;
            _sessaoInterface = sessaoInterface;
        }
        public async Task<Response<Usuario>> Login(UsuarioLoginDto usuarioLoginDto)
        {
            Response<Usuario> response = new Response<Usuario>();
            try
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Email == usuarioLoginDto.Email);
                if (usuario == null)
                {
                    response.Status = false;
                    response.Mensagem = "Usuário não encontrado.";
                    return response;
                }
                if (!_senhaInterface.VerificarSenhaHash(usuarioLoginDto.Senha, usuario.SenhaHash, usuario.SenhaSalt))
                {
                    response.Status = false;
                    response.Mensagem = "Senha incorreta.";
                    return response;
                }

                usuario.DataUltimoAcesso = DateTime.Now;
                _context.Usuarios.Update(usuario);
                await _context.SaveChangesAsync();

                response.Status = true;
                response.Mensagem = "Login realizado com sucesso.";
                response.Dados = usuario;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = $"Erro: {ex.Message}";
            }

            return response;
        }


        public async Task<Response<Usuario>> RegistrarUsuario(UsuarioRegisterDto usuarioRegisterDto)
        {
            Response<Usuario> response = new Response<Usuario>();
            try
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Email == usuarioRegisterDto.Email);
                if (usuario != null)
                {
                    response.Status = false;
                    response.Mensagem = "Usuário já existe.";
                    return response;
                }

                _senhaInterface.CriarSenhaHash(usuarioRegisterDto.Senha, out byte[] senhaHash, out byte[] senhaSalt);
                usuario = new Usuario()
                {
                    Nome = usuarioRegisterDto.Nome,
                    Email = usuarioRegisterDto.Email,
                    SenhaHash = senhaHash,
                    SenhaSalt = senhaSalt,
                    DataUltimoAcesso = DateTime.Now
                };
                await _context.Usuarios.AddAsync(usuario);
                await _context.SaveChangesAsync();
                response.Status = true;
                response.Mensagem = $"Usuário {usuarioRegisterDto.Nome} registrado com sucesso.";
                response.Dados = usuario; 
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = $"Erro: {ex.Message}";
            }

            return response; 
        }
    }
   }

