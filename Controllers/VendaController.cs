using GestVeicular.Models;
using GestVeicular.Services.ClienteService;
using GestVeicular.Services.SessaoService;
using GestVeicular.Services.VeiculoService;
using GestVeicular.Services.VendaService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestVeicular.Controllers
{
    public class VendaController : Controller
    {
        private readonly IVendaInterface _vendaInterface;
        private readonly ISessaoInterface _sessaoInterface;
        private readonly IClienteInterface _clienteInterface;
        private readonly IVeiculoInterface _veiculoInterface;

        public VendaController(
            IVendaInterface vendaInterface,
            ISessaoInterface sessaoInterface,
            IClienteInterface clienteInterface,
            IVeiculoInterface veiculoInterface)
        {
            _vendaInterface = vendaInterface;
            _sessaoInterface = sessaoInterface;
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

            var response = await _vendaInterface.ListarVendas();
            if (!response.Status)
            {
                TempData["MensagemErro"] = response.Mensagem;
                return View(new List<Venda>());
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
        public async Task<IActionResult> Cadastrar(Venda vendas)
        {
            if (!ModelState.IsValid)
            {
                await PreencherViewBagsAsync();
                return View(vendas);
            }

            var response = await _vendaInterface.AdicionarVenda(vendas);

            if (!response.Status)
            {
                TempData["MensagemErro"] = response.Mensagem;
                await PreencherViewBagsAsync();
                return View(vendas);
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

            var vendaResponse = await _vendaInterface.BuscarVendaPorId(id);
            if (!vendaResponse.Status)
            {
                TempData["MensagemErro"] = vendaResponse.Mensagem;
                return RedirectToAction("Index");
            }

            await PreencherViewBagsAsync();
            return View(vendaResponse.Dados);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Venda venda)
        {
            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
                return RedirectToAction("Login", "Login");

            if (!ModelState.IsValid)
            {
                await PreencherViewBagsAsync();
                return View(venda);
            }

            var response = await _vendaInterface.AtualizarVenda(venda);
            if (!response.Status)
            {
                TempData["MensagemErro"] = response.Mensagem ?? "Erro ao atualizar a venda.";
                await PreencherViewBagsAsync();
                return View(venda);
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

            var vendaResponse = await _vendaInterface.BuscarVendaPorId(id); 
            if (!vendaResponse.Status || vendaResponse.Dados == null)
            {
                TempData["MensagemErro"] = vendaResponse.Mensagem ?? "Venda não encontrada.";
                return RedirectToAction("Index");
            }

            var venda = vendaResponse.Dados;
            venda.Cliente = (await _clienteInterface.BuscarClientePorId(venda.ClienteId))?.Dados;
            venda.Veiculo = (await _veiculoInterface.BuscarVeiculoPorId(venda.VeiculoId))?.Dados;

            return View(venda);
        }


        [HttpPost, ActionName("Deletar")]
        public async Task<IActionResult> DeletarPost(int id)
        {
            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
                return RedirectToAction("Login", "Login");

            var vendaResponse = await _vendaInterface.BuscarVendaPorId(id);
            if (!vendaResponse.Status || vendaResponse.Dados == null)
            {
                TempData["MensagemErro"] = vendaResponse.Mensagem ?? "Venda não encontrada.";
                return RedirectToAction("Index");
            }

            var response = await _vendaInterface.DeletarVenda(id);
            if (response.Status)
            {
                TempData["MensagemSucesso"] = response.Mensagem;
                return RedirectToAction("Index");
            }

            TempData["MensagemErro"] = response.Mensagem ?? "Erro ao tentar excluir a venda.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Detalhes(int id)
        {
            var response = await _vendaInterface.BuscarVendaPorId(id);
            if (!response.Status)
            {
                TempData["MensagemErro"] = response.Mensagem;
                return RedirectToAction("Index");
            }

            return View(response.Dados);
        }
    }
}
