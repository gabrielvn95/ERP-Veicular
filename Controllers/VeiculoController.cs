using GestVeicular.Data;
using GestVeicular.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestVeicular.Controllers
{
    public class VeiculoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Veiculo _veiculo;
        public VeiculoController(ApplicationDbContext context, Veiculo veiculo)
        {
            _context = context;
            _veiculo = veiculo;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Detalhes(int id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Deletar(int id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Listar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<Response<Veiculo>> DetalhesVeiculo(int id)
        {
            Response<Veiculo> response = new Response<Veiculo>();
            try
            {
                var veiculo = await _context.Veiculos.FindAsync(id);
                if (veiculo == null)
                {
                    response.Status = false;
                    response.Mensagem = "Veículo não encontrado.";
                    return response;
                }
                response.Status = true;
                response.Mensagem = $"Veículo {veiculo.IdVeiculo} encontrado com sucesso.";
                response.Dados = veiculo;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = ex.Message;
            }
            return response;

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<Response<Veiculo>> CriarVeiculo(Veiculo veiculo)
        {
            Response<Veiculo> response = new Response<Veiculo>();
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Veiculos.Add(veiculo);
                    await _context.SaveChangesAsync();
                    response.Status = true;
                    response.Mensagem = $"Veículo {veiculo}, criado com sucesso.";
                    response.Dados = veiculo;
                }
                else
                {
                    response.Status = false;
                    response.Mensagem = "Erro ao criar veículo.";
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = ex.Message;
            }
            return response;

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<Response<Veiculo>> EditarVeiculo(int id)
        {
            Response<Veiculo> response = new Response<Veiculo>();
            try
            {
                var veiculo = await _context.Veiculos.FindAsync(id);
                if (veiculo == null)
                {
                    response.Status = false;
                    response.Mensagem = "Veículo não encontrado.";
                    return response;
                }
                if (ModelState.IsValid)
                {
                    _context.Veiculos.Update(veiculo);
                    await _context.SaveChangesAsync();
                    response.Status = true;
                    response.Mensagem = $"Veículo {veiculo}, editado com sucesso.";
                    response.Dados = veiculo;
                }
                else
                {
                    response.Status = false;
                    response.Mensagem = "Erro ao editar veículo.";
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = ex.Message;
            }

            return response;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<Response<Veiculo>> DeletarVeiculo(int id)
        {
            Response<Veiculo> response = new Response<Veiculo>();
            try
            {
                var veiculo = await _context.Veiculos.FindAsync(id);
                if (veiculo == null)
                {
                    response.Status = false;
                    response.Mensagem = "Veículo não encontrado.";
                    return response;
                }
                _context.Veiculos.Remove(veiculo);
                await _context.SaveChangesAsync();
                response.Status = true;
                response.Mensagem = $"Veículo {veiculo}, deletado com sucesso.";
                response.Dados = veiculo;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = ex.Message;
            }
            return response;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<Response<Veiculo>> ListarVeiculos(Veiculo veiculo)
        {
            Response<Veiculo> response = new Response<Veiculo>();
            try
            {
                var veiculos = await _context.Veiculos.ToListAsync();
                if (veiculos == null || !veiculos.Any())
                {
                    response.Status = false;
                    response.Mensagem = "Nenhum veículo encontrado.";
                    return response;
                }
                response.Status = true;
                response.Mensagem = "Veículos encontrados com sucesso.";
                response.Dados = veiculo;

            }

            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = ex.Message;
            }
            return response;

        }
    }
}
