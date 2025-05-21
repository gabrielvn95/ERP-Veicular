using GestVeicular.Data;
using GestVeicular.Models;

namespace GestVeicular.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly ApplicationDbContext _context;
        public UsuarioRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }
        public Usuario Adicionar(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            return usuario;
        }

        public Usuario AlterarSenha(AlterarSenha alterarSenha)
        {
            Usuario usuarioDB = BuscarPorId(alterarSenha.Id);
            if (usuarioDB == null)
            {
              throw new Exception("Usuário não encontrado!");
            }

            if(!VerificaSenhaHash(alterarSenha.SenhaAtual, usuarioDB.SenhaHash, usuarioDB.SenhaSalt))
            {
                throw new Exception("Senha atual não confere!");
            }

            if(VerificaSenhaHash(alterarSenha.NovaSenha, usuarioDB.SenhaHash, usuarioDB.SenhaSalt))
            {
                throw new Exception("A nova senha não pode ser igual a senha atual!");
            }

            CriarSenhaHash(alterarSenha.NovaSenha, out byte[] senhaHash, out byte[] senhaSalt);
            usuarioDB.SenhaHash = senhaHash;
            usuarioDB.SenhaSalt = senhaSalt;

            _context.Usuarios.Update(usuarioDB);
            _context.SaveChanges();

            return usuarioDB;
        }

        private bool VerificaSenhaHash(string senha, byte[] senhaHashArmazenada, byte[] senhaSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(senhaSalt))
            {
                var HashCalculada = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
                return HashCalculada.SequenceEqual(senhaHashArmazenada);
            }
        }

        private void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                senhaSalt = hmac.Key;
                senhaHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
            }
        }

        public Usuario Atualizar(Usuario usuario)
        {
            Usuario usuarioDb = BuscarPorId((int)usuario.IdUsuario);

            if(usuarioDb == null)
            {
                throw new Exception("Usuário não encontrado!");
            }

            usuarioDb.Nome = usuario.Nome;
            usuarioDb.Email = usuario.Email;

            _context.Usuarios.Update(usuarioDb);
            _context.SaveChanges();
            return usuarioDb;
        }

        public Usuario BuscarPorEmail(string email)
        {
            return _context.Usuarios.FirstOrDefault(x => x.Email.ToUpper() == email.ToUpper());
        }

        public Usuario BuscarPorId(int id)
        {
            return _context.Usuarios.FirstOrDefault(x => x.IdUsuario == id);
        }

        public List<Usuario> BuscarTodosUsuarios()
        {
            return _context.Usuarios.ToList();
        }

        public bool Apagar(int id)
        {
            Usuario usuarioDB = BuscarPorId(id);
            if (usuarioDB == null)
            {
                throw new Exception("Usuário não encontrado!");
            }

            _context.Remove(usuarioDB);
            _context.SaveChanges();

            return true;
        }
    }
}
