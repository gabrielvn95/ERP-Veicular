using GestVeicular.Data;
using GestVeicular.Models;
using Microsoft.EntityFrameworkCore;

namespace GestVeicular.Services.ClienteService
{
    public class ClienteService : IClienteInterface
    {
        private readonly ApplicationDbContext _context;

        public ClienteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<Cliente>> AdicionarCliente(Cliente cliente)
        {
            Response<Cliente> response = new Response<Cliente>();
            try
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                response.Status = true;
                response.Mensagem = "Cliente adicionado com sucesso.";
                response.Dados = cliente;
                return response;

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = $"Erro: {ex.Message}";
                return response;
            }
        }

        public Task<Response<Cliente>> AtualizarCliente(Cliente cliente)
        {
            Response<Cliente> response = new Response<Cliente>();
            try
            {
                _context.Update(cliente);
                _context.SaveChangesAsync();
                response.Status = true;
                response.Mensagem = "Cliente atualizado com sucesso.";
                response.Dados = cliente;
                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = $"Erro: {ex.Message}";
                return Task.FromResult(response);
            }
        }

        public Task<Response<Cliente>> BuscarClientePorId(int idCliente)
        {
            throw new NotImplementedException();
        }

        public Task<Response<Cliente>> DeletarCliente(int idCliente)
        {
            Response<Cliente> response = new Response<Cliente>();
            try
            {
                var cliente = _context.Clientes.Find(idCliente);
                if (cliente == null)
                {
                    response.Status = false;
                    response.Mensagem = "Cliente não encontrado.";
                    return Task.FromResult(response);
                }
                _context.Clientes.Remove(cliente);
                _context.SaveChangesAsync();
                response.Status = true;
                response.Mensagem = "Cliente deletado com sucesso.";
                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = $"Erro: {ex.Message}";
                return Task.FromResult(response);
            }
        }

        public Task<Response<Cliente>> Detalhes(int idCliente)
        {
            Response<Cliente> response = new Response<Cliente>();
            try
            {
                var cliente = _context.Clientes.Find(idCliente);
                if (cliente == null)
                {
                    response.Status = false;
                    response.Mensagem = "Cliente não encontrado.";
                    return Task.FromResult(response);
                }
                response.Status = true;
                response.Mensagem = "Cliente encontrado com sucesso.";
                response.Dados = cliente;
                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = $"Erro: {ex.Message}";
                return Task.FromResult(response);
            }
        }

        public async Task<Response<List<Cliente>>> ListarClientes()
        {
            var response = new Response<List<Cliente>>();
            try
            {
                var clientes = await _context.Clientes.ToListAsync();
                response.Status = true;
                response.Dados = clientes;
                response.Mensagem = "Clientes encontrados com sucesso.";
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = $"Erro: {ex.Message}";

            }
            return response;
        }
    }
}
