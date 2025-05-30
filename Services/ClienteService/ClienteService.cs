using GestVeicular.Data;
using GestVeicular.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            var response = new Response<Cliente>();
            try
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                response.Status = true;
                response.Mensagem = "Cliente adicionado com sucesso.";
                response.Dados = cliente;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = $"Erro: {ex.Message}";
            }
            return response;
        }

        public async Task<Response<Cliente>> AtualizarCliente(Cliente cliente)
        {
            var response = new Response<Cliente>();
            try
            {
                _context.Update(cliente);
                await _context.SaveChangesAsync();
                response.Status = true;
                response.Mensagem = "Cliente atualizado com sucesso.";
                response.Dados = cliente;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = $"Erro: {ex.Message}";
            }
            return response;
        }

        public async Task<Response<Cliente>> BuscarClientePorId(int idCliente)
        {
            var response = new Response<Cliente>();
            try
            {
                var cliente = await _context.Clientes.FindAsync(idCliente);
                if (cliente == null)
                {
                    response.Status = false;
                    response.Mensagem = "Cliente não encontrado.";
                }
                else
                {
                    response.Status = true;
                    response.Mensagem = "Cliente encontrado com sucesso.";
                    response.Dados = cliente;
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = $"Erro: {ex.Message}";
            }
            return response;
        }

        public async Task<Response<Cliente>> DeletarCliente(int idCliente)
        {
            var response = new Response<Cliente>();
            try
            {
                var cliente = await _context.Clientes.FindAsync(idCliente);
                if (cliente == null)
                {
                    response.Status = false;
                    response.Mensagem = "Cliente não encontrado.";
                    return response;
                }
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
                response.Status = true;
                response.Mensagem = "Cliente deletado com sucesso.";
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = $"Erro: {ex.Message}";
            }
            return response;
        }

        public async Task<Response<Cliente>> Detalhes(int idCliente)
        {
            var response = new Response<Cliente>();
            try
            {
                var cliente = await _context.Clientes.FindAsync(idCliente);
                if (cliente == null)
                {
                    response.Status = false;
                    response.Mensagem = "Cliente não encontrado.";
                }
                else
                {
                    response.Status = true;
                    response.Mensagem = "Cliente encontrado com sucesso.";
                    response.Dados = cliente;
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = $"Erro: {ex.Message}";
            }
            return response;
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
