using GestVeicular.Models;

namespace GestVeicular.Services.SessaoService
{
    public interface ISessaoInterface
    {
        Usuario BuscarSessao();
        void CriarSessao(Usuario usuario);
        void RemoverSessao();
    }
}
