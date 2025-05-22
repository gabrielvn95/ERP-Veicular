using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GestVeicular.Models;
using GestVeicular.Data;
using GestVeicular.Services.SessaoService;
using GestVeicular.Services.ServicosService;

namespace GestVeicular.Controllers
{

    public class ServicosController : Controller
    {
        private readonly ISessaoInterface _sessaoInterface;
        private readonly IServicosInterface _servicosInterface;

        public ServicosController(ISessaoInterface sessaoInterface, IServicosInterface servicosInterface)
        {
            _sessaoInterface = sessaoInterface;
            _servicosInterface = servicosInterface;
        }

        public async Task<IActionResult> Index()
        {
            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var servicos = await _servicosInterface.BuscarTodosServicos();
            return View(servicos.Dados);

        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var servico = await _servicosInterface.BuscarServicoPorId(id);
            return View(servico.Dados);
        }

        [HttpGet]
        public async Task<IActionResult> Deletar(int id)
        {
            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var servico = await _servicosInterface.BuscarServicoPorId(id);
            return View(servico.Dados);
        }

        [HttpGet]
        public async Task<IActionResult> Detalhes(int id)
        {
            var response = await _servicosInterface.BuscarServicoPorId(id);
            if (!response.Status)
            {
                TempData["MensagemErro"] = response.Mensagem;
                return RedirectToAction("Index");
            }

            return View(response.Dados);

        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(Servicos servico)
        {
            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }
            if (ModelState.IsValid)
            {
                var response = await _servicosInterface.AdicionarServico(servico);
                if (response.Status)
                {
                    TempData["MensagemSucesso"] = response.Mensagem;
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["MensagemErro"] = response.Mensagem;
                    return View(servico);
                }
            }
            return View(servico);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Servicos servico)
        {
            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }
            if (ModelState.IsValid)
            {
                var response = await _servicosInterface.AtualizarServico(servico);
                if (response.Status)
                {
                    TempData["MensagemSucesso"] = response.Mensagem;
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["MensagemErro"] = response.Mensagem;
                    return View(servico);
                }
            }
            return View(servico);
        }

        [HttpPost]
        public async Task<IActionResult> Deletar(Servicos servico)
        {
            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }
            if (ModelState.IsValid)
            {
                var response = await _servicosInterface.DeletarServico(servico.IdServico);
                if (response.Status)
                {
                    TempData["MensagemSucesso"] = response.Mensagem;
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["MensagemErro"] = response.Mensagem;
                    return View(servico);
                }
            }
            return View(servico);

        }
    }
}
