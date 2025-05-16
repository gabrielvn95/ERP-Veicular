using GestVeicular.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace GestVeicular.Services.SessaoService
{
    public class SessaoService : ISessaoInterface
    {
       private readonly IHttpContextAccessor _ContextAccessor;

        public SessaoService(IHttpContextAccessor httpContextAccessor)
        {
            _ContextAccessor = httpContextAccessor;
        }
        public Usuario BuscarSessao()
        {
            var SessaoUsuario = _ContextAccessor.HttpContext.Session.GetString("UsuarioLogado");
            if(string.IsNullOrEmpty(SessaoUsuario))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<Usuario>(SessaoUsuario);
        }

        public void CriarSessao(Usuario usuario)
        {
            var usuarioJson = JsonConvert.SerializeObject(usuario);
            _ContextAccessor.HttpContext.Session.SetString("UsuarioLogado", usuarioJson);
        }

        public void RemoverSessao()
        {
            _ContextAccessor.HttpContext.Session.Remove("UsuarioLogado");
        }
    }
}
