using GestVeicular.Models;
using GestVeicular.Services.SessaoService;
using GestVeicular.Services.VeiculoService;
using Microsoft.AspNetCore.Mvc;


namespace GestVeicular.Controllers
{
    public class VeiculoController : Controller
    {
        private readonly IVeiculoInterface _veiculoInterface;
        private readonly ISessaoInterface _sessaoInterface;

        public VeiculoController(IVeiculoInterface veiculoInterface, ISessaoInterface sessaoInterface)
        {
            _veiculoInterface = veiculoInterface;
            _sessaoInterface = sessaoInterface;
        }

        public async Task<IActionResult> Index()
        {
            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }

            var veiculos = await _veiculoInterface.ListarVeiculos();

            if (!veiculos.Status || veiculos.Dados == null)
            {
                TempData["MensagemErro"] = veiculos.Mensagem ?? "Erro ao carregar veículos.";
                return View(new List<Veiculo>()); 
            }

            return View(veiculos.Dados);
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
            var veiculo = await _veiculoInterface.BuscarVeiculoPorId(id);
            return View(veiculo.Dados);
        }

        [HttpGet]
        public async Task<IActionResult> Deletar(int id)
        {
            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var veiculo = await _veiculoInterface.BuscarVeiculoPorId(id);
            return View(veiculo.Dados);
        }

        [HttpGet]
        public async Task<IActionResult> Detalhes(int id)
        {
            var response = await _veiculoInterface.Detalhes(id);
            if (!response.Status)
            {
                TempData["MensagemErro"] = response.Mensagem;
                return RedirectToAction("Index");
            }

            return View(response.Dados);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(Veiculo veiculo)
        {
            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }
            if (ModelState.IsValid)
            {
                var response = await _veiculoInterface.AdicionarVeiculo(veiculo);
                if (response.Status)
                {
                    TempData["MensagemSucesso"] = response.Mensagem;
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["MensagemErro"] = response.Mensagem;
                }
            }
            return View(veiculo);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Veiculo veiculo)
        {
            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }
            if (ModelState.IsValid)
            {
                var response = await _veiculoInterface.AtualizarVeiculo(veiculo);
                if (response.Status)
                {
                    TempData["MensagemSucesso"] = response.Mensagem;
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["MensagemErro"] = response.Mensagem;
                }
            }
            return View(veiculo);
        }

        [HttpPost]
        public async Task<IActionResult> Deletar(Veiculo veiculo)
        {
            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }
            if (ModelState.IsValid)
            {
                var response = await _veiculoInterface.DeletarVeiculo(veiculo.IdVeiculo);
                if (response.Status)
                {
                    TempData["MensagemSucesso"] = response.Mensagem;
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["MensagemErro"] = response.Mensagem;
                }
            }
            return View(veiculo);
        }
       
        }
    }

