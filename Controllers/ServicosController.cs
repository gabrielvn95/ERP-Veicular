using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;
using GestVeicular.Models;
using GestVeicular.Services.SessaoService;
using GestVeicular.Services.ServicosService;
using GestVeicular.Services.ClienteService;
using GestVeicular.Services.VeiculoService;

namespace GestVeicular.Controllers
{
    public class ServicosController : Controller
    {
        private readonly ISessaoInterface _sessaoInterface;
        private readonly IServicosInterface _servicosInterface;
        private readonly IClienteInterface _clienteInterface;
        private readonly IVeiculoInterface _veiculoInterface;

        public ServicosController(
            ISessaoInterface sessaoInterface,
            IServicosInterface servicosInterface,
            IClienteInterface clienteInterface,
            IVeiculoInterface veiculoInterface)
        {
            _sessaoInterface = sessaoInterface;
            _servicosInterface = servicosInterface;
            _clienteInterface = clienteInterface;
            _veiculoInterface = veiculoInterface;
        }

        private async Task PreencherViewBagsAsync()
        {
            var clientesResponse = await _clienteInterface.ListarClientes();
            var veiculosResponse = await _veiculoInterface.ListarVeiculos();

            ViewBag.Clientes = (clientesResponse.Dados ?? new List<Cliente>())
                .Select(c => new SelectListItem { Value = c.IdCliente.ToString(), Text = c.Nome })
                .ToList();

            ViewBag.Veiculos = (veiculosResponse.Dados ?? new List<Veiculo>())
                .Select(v => new SelectListItem { Value = v.IdVeiculo.ToString(), Text = v.NomeVeiculo + " - " + v.Placa })
                .ToList();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
                return RedirectToAction("Login", "Login");

            var response = await _servicosInterface.BuscarTodosServicos();
            if (!response.Status)
            {
                TempData["MensagemErro"] = response.Mensagem;
                return View();
            }

            return View(response.Dados);
        }

        [HttpGet]
        public async Task<IActionResult> Cadastrar()
        {
            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
                return RedirectToAction("Login", "Login");

            await PreencherViewBagsAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(Servicos servico)
        {
            if (!ModelState.IsValid)
            {
                await PreencherViewBagsAsync();
                return View(servico);
            }

            var response = await _servicosInterface.AdicionarServico(servico);

            if (!response.Status)
            {
                TempData["MensagemErro"] = response.Mensagem;
                await PreencherViewBagsAsync();
                return View(servico);
            }

            TempData["MensagemSucesso"] = response.Mensagem;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
                return RedirectToAction("Login", "Login");

            var servicoResponse = await _servicosInterface.BuscarServicoPorId(id);
            if (!servicoResponse.Status)
            {
                TempData["MensagemErro"] = servicoResponse.Mensagem;
                return RedirectToAction("Index");
            }

            await PreencherViewBagsAsync();
            return View(servicoResponse.Dados);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Servicos servico)
        {
            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
                return RedirectToAction("Login", "Login");

            if (!ModelState.IsValid)
            {
                await PreencherViewBagsAsync();
                return View(servico);
            }

            var response = await _servicosInterface.AtualizarServico(servico);
            if (!response.Status)
            {
                TempData["MensagemErro"] = response.Mensagem ?? "Erro ao atualizar o serviço.";
                await PreencherViewBagsAsync();
                return View(servico);
            }

            TempData["MensagemSucesso"] = response.Mensagem;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Deletar(int id)
        {
            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
                return RedirectToAction("Login", "Login");

            var servicoResponse = await _servicosInterface.BuscarServicoPorId(id);
            if (!servicoResponse.Status || servicoResponse.Dados == null)
            {
                TempData["MensagemErro"] = servicoResponse.Mensagem;
                return RedirectToAction("Index");
            }

            var servico = servicoResponse.Dados;
            servico.Cliente = (await _clienteInterface.BuscarClientePorId(servico.ClienteId))?.Dados;
            servico.Veiculo = (await _veiculoInterface.BuscarVeiculoPorId(servico.VeiculoId))?.Dados;

            return View(servico);
        }

        [HttpPost, ActionName("Deletar")]
        public async Task<IActionResult> DeletarPost(int id)
        {
            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
                return RedirectToAction("Login", "Login");

            var servicoResponse = await _servicosInterface.BuscarServicoPorId(id);
            if (!servicoResponse.Status || servicoResponse.Dados == null)
            {
                TempData["MensagemErro"] = servicoResponse.Mensagem ?? "Serviço não encontrado.";
                return RedirectToAction("Index");
            }

            var response = await _servicosInterface.DeletarServico(id);
            if (response.Status)
            {
                TempData["MensagemSucesso"] = response.Mensagem;
                return RedirectToAction("Index");
            }

            TempData["MensagemErro"] = response.Mensagem ?? "Erro ao tentar excluir o serviço.";
            return RedirectToAction("Index");
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
    }
}
