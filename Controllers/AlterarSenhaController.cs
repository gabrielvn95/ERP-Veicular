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
                _sessaoInterface = sessaoInterface;
                _usuarioRepositorio = usuarioRepositorio;
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
                alterarSenha.Id = usuario.IdUsuario;

                if (ModelState.IsValid)
                {
                    _usuarioRepositorio.AlterarSenha(alterarSenha);
                    TempData["MensagemSucesso"] = "Senha alterada com sucesso!";
                    return RedirectToAction("Alterar");
                }
                return View("Alterar", alterarSenha);

            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = "Erro ao alterar a senha: " + ex.Message;
                return RedirectToAction("Alterar");
            }

        }
    }
}
