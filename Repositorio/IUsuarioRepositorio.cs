using GestVeicular.Models;

namespace GestVeicular.Repositorio
{
    public interface IUsuarioRepositorio
    {
        Usuario BuscarPorEmail (string email);
        List<Usuario> BuscarTodosUsuarios();
        Usuario BuscarPorId(int id);
        Usuario Adicionar(Usuario usuario);
        Usuario Atualizar(Usuario usuario);
        Usuario AlterarSenha(AlterarSenha alterarSenha);
        
        bool Apagar(int id);
    }
}
