using GestVeicular.Models;
using GestVeicular.Services.ClienteService;
using GestVeicular.Services.SessaoService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestVeicular.Controllers
{
    public class ClienteController : Controller
    {
     private readonly IClienteInterface _clienteInterface;
     private readonly ISessaoInterface _sessaoInterface;

        public ClienteController(IClienteInterface clienteInterface, ISessaoInterface sessaoInterface)
        {
            _clienteInterface = clienteInterface;
            _sessaoInterface = sessaoInterface;
        }

        public async Task<IActionResult> Index()
        {
            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var clientes = await _clienteInterface.ListarClientes();
            return View(clientes.Dados);
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
            var cliente = await _clienteInterface.BuscarClientePorId(id);
            return View(cliente.Dados);
        }

        [HttpGet]
        public async Task<IActionResult> Deletar(int id)
        {
            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var cliente = await _clienteInterface.BuscarClientePorId(id);
            return View(cliente.Dados);
        }

        [HttpGet]
        public async Task<IActionResult> Detalhes(int id)
        {
            var response = await _clienteInterface.Detalhes(id);

            if (!response.Status)
            {
                TempData["MensagemErro"] = response.Mensagem;   
                return RedirectToAction("Index");
            }

            return View(response.Dados);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(Cliente cliente)
        {
            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }
            if (ModelState.IsValid)
            {
                var response = await _clienteInterface.AdicionarCliente(cliente);
                if (response.Status)
                {
                    TempData["MensagemSucesso"] = response.Mensagem;
                    return RedirectToAction("Index");
                }
                TempData["MensagemErro"] = response.Mensagem;
            }
            return View(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Cliente cliente)
        {
            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }
            if (ModelState.IsValid)
            {
                var response = await _clienteInterface.AtualizarCliente(cliente);
                if (response.Status)
                {
                    TempData["MensagemSucesso"] = response.Mensagem;
                    return RedirectToAction("Index");
                }
                TempData["MensagemErro"] = response.Mensagem;
            }
            return View(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Deletar(Cliente cliente)
        {
            if (cliente == null)
            {
                TempData["MensagemErro"] = "Cliente não encontrado.";
                return View(cliente);
            }
            var clienteResult = await _clienteInterface.BuscarClientePorId(cliente.IdCliente);
            if (clienteResult.Status)
            {
                TempData["MensagemSucesso"] = clienteResult.Mensagem;
            }else
            {
                TempData["MensagemErro"] = clienteResult.Mensagem;
                return View(clienteResult);
            }

            return RedirectToAction("Index");

        }

        }
}

