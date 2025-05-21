using GestVeicular.DTO;
using GestVeicular.Repositorio;
using GestVeicular.Services.LoginService;
using GestVeicular.Services.SessaoService;
using Microsoft.AspNetCore.Mvc;

namespace GestVeicular.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ILoginInterface _loginInterface;
        private readonly ISessaoInterface _sessaoInterface;
        public LoginController(IUsuarioRepositorio usuarioRepositorio, ILoginInterface loginInterface, ISessaoInterface sessaoInterface)
        {
            _loginInterface = loginInterface;
            _usuarioRepositorio = usuarioRepositorio;
            _sessaoInterface = sessaoInterface;
        }

        public IActionResult Login()
        {
            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult Registrar()
        {
            return View();
        }

        public IActionResult Logout()
        {
            _sessaoInterface.RemoverSessao();
            return RedirectToAction("Login", "Login");
        }

        public IActionResult RedefinirSenha()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UsuarioLoginDto usuarioLoginDto)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _loginInterface.Login(usuarioLoginDto);
                if (usuario.Status)
                {
                    TempData["MensagemSucesso"] = usuario.Mensagem;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["MensagemErro"] = usuario.Mensagem;
                }
            }
            return View(usuarioLoginDto);
        }

        public async Task<IActionResult> Registrar(UsuarioRegisterDto usuarioRegisterDto)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _loginInterface.RegistrarUsuario(usuarioRegisterDto, Enums.TipoUsuario.Padrao);
                if (usuario.Status)
                {
                    TempData["MensagemSucesso"] = usuario.Mensagem;
                    return RedirectToAction("Login", "Login");
                }
                else
                {
                    TempData["MensagemErro"] = usuario.Mensagem;
                }
            }
            return View(usuarioRegisterDto);

        }

    }
}

