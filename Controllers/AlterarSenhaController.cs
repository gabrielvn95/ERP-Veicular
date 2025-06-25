using GestVeicular.Models;
using GestVeicular.Repositorio;
using GestVeicular.Services.SessaoService;
using Microsoft.AspNetCore.Mvc;

namespace GestVeicular.Controllers
{
    public class AlterarSenhaController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessaoInterface _sessaoInterface;

        public AlterarSenhaController(IUsuarioRepositorio usuarioRepositorio, ISessaoInterface sessaoInterface)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessaoInterface = sessaoInterface;
        }

        [HttpGet]
        public IActionResult Alterar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Alterar(AlterarSenha alterarSenha)
        {
            try
            {
                var usuario = _sessaoInterface.BuscarSessao();

                if (usuario == null)
                {
                    TempData["MensagemErro"] = "Usuário não está logado.";
                    return RedirectToAction("Login", "Login");
                }

                alterarSenha.Id = usuario.IdUsuario;

                if (ModelState.IsValid)
                {
                    _usuarioRepositorio.AlterarSenha(alterarSenha);
                    TempData["MensagemSucesso"] = "Senha alterada com sucesso!";
                    return RedirectToAction("Index", "Home");
                }

                return View(alterarSenha); 
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = "Erro ao alterar a senha: " + ex.Message;
                return RedirectToAction("Alterar");
            }
        }
    }
}
