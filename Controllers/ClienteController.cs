using GestVeicular.Data;
using GestVeicular.Models;
using Microsoft.AspNetCore.Mvc;

namespace GestVeicular.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Cliente _cliente;
        public ClienteController(ApplicationDbContext context, Cliente cliente)
        {
            _cliente = cliente;
            _context = context;
        }
        public IActionResult Index()
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
        public async Task<Response<Cliente>> Criar(Cliente cliente)
        {
            Response<Cliente> response = new Response<Cliente>();
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Clientes.Add(cliente);
                    await _context.SaveChangesAsync();
                    response.Status = true;
                    response.Mensagem = $"Cliente {cliente.Nome} criado com sucesso.";
                }
                else
                {
                    response.Status = false;
                    response.Mensagem = "Erro ao cadastrar cliente.";
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
        public async Task<Response<Cliente>> Detalhes(int id)
        {
            Response<Cliente> response = new Response<Cliente>();
            try
            {
                var cliente = await _context.Clientes.FindAsync(id);
                if (cliente == null)
                {
                    response.Status = false;
                    response.Mensagem = "Cliente não encontrado.";
                    return response;
                }
                response.Status = true;
                response.Dados = cliente;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = $"Erro: {ex.Message}";
            }
            return response;

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<Response<Cliente>> Editar(Cliente cliente)
        {
            Response<Cliente> response = new Response<Cliente>();
            try
            {
                var clienteExistente = await _context.Clientes.FindAsync(cliente.IdCliente);
                if(ModelState.IsValid)
                    {
                    if (clienteExistente != null)
                    {
                        clienteExistente.Nome = cliente.Nome;
                        clienteExistente.Cpf = cliente.Cpf;
                        clienteExistente.Telefone = cliente.Telefone;
                        _context.Clientes.Update(clienteExistente);
                        await _context.SaveChangesAsync();
                        response.Status = true;
                        response.Mensagem = $"Cliente {cliente.Nome} editado com sucesso.";
                    }
                    else
                    {
                        response.Status = false;
                        response.Mensagem = "Cliente não encontrado.";
                    }
                }
                catch
            {
                response.Status = false;
                response.Mensagem = $"Erro ao editar cliente {}.";
            }


            }
        }
    }
}
