using GestVeicular.Data;
using GestVeicular.Enums;
using GestVeicular.Models;
using Microsoft.EntityFrameworkCore;

namespace GestVeicular.Services.ServicosService
{
    public class ServicosService : IServicosInterface
    {
        private readonly ApplicationDbContext _context;
        public ServicosService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Response<Servicos>> AdicionarServico(Servicos servico)
        {
            Response<Servicos> response = new Response<Servicos>();
            try
            {
                await _context.AddAsync(servico);
                await _context.SaveChangesAsync();
                response.Status = true;
                response.Mensagem = "Serviço adicionado com sucesso.";
                response.Dados = servico;
                return response;
            } catch(Exception ex)
            {
                response.Status = false;
                response.Mensagem = $"Erro: {ex.Message}";
                return response;
            }

        }

        public async Task<Response<Servicos>> AtualizarServico(Servicos servico)
        {
            Response<Servicos> response = new Response<Servicos>();
            try
            {
                _context.Update(servico);
                await _context.SaveChangesAsync();
                response.Status = true;
                response.Mensagem = "Serviço atualizado com sucesso.";
                response.Dados = servico;
                return response;
            } catch(Exception ex)
            {
                response.Status = false;
                response.Mensagem = $"Erro: {ex.Message}";
                return response;
            }

        }

        public async Task<Response<Servicos>> AtualizarStatusVenda(int idServico, StatusServicos novoStatus)
        {
            Response<Servicos> response = new Response<Servicos>();
            try
            {
                var servicoExistente = await _context.Servicos.FindAsync(idServico);
                if (servicoExistente == null)
                {
                    response.Status = false;
                    response.Mensagem = "Serviço não encontrado.";
                    return response;
                }

                servicoExistente.Status = novoStatus;
                servicoExistente.DataUltimaAtualizacao = DateTime.Now;

                _context.Servicos.Update(servicoExistente);
                await _context.SaveChangesAsync();

                response.Status = true;
                response.Mensagem = "Status do serviço atualizado com sucesso.";
                response.Dados = servicoExistente;
                return response;

            } catch(Exception ex)
            {
                response.Status = false;
                response.Mensagem = $"Erro: {ex.Message}";
                return response;
            }
        }

        public async Task<Response<Servicos>> BuscarServicoPorId(int idServico)
        {
            Response<Servicos> response = new Response<Servicos>();
            try
            {
                var servico = await _context.Servicos.FindAsync(idServico);
                if (servico == null)
                {
                    response.Status = false;
                    response.Mensagem = "Serviço não encontrado.";
                    return response;
                }
                response.Status = true;
                response.Mensagem = "Serviço encontrado com sucesso.";
                response.Dados = servico;
                return response;
            } catch(Exception ex)
            {
                response.Status = false;
                response.Mensagem = $"Erro: {ex.Message}";
                return response;
            }

        }

        public async Task<Response<Servicos>> BuscarServicosPorId(int idServico)
        {
            Response<Servicos> response = new Response<Servicos>();
            try
                {
                var servico = await _context.Servicos.FindAsync(idServico);
                if (servico == null)
                {
                    response.Status = false;
                    response.Mensagem = "Serviço não encontrado.";
                    return response;
                }
                response.Status = true;
                response.Mensagem = "Serviço encontrado com sucesso.";
                response.Dados = servico;
                return response;
            } catch(Exception ex)
            {
                response.Status = false;
                response.Mensagem = $"Erro: {ex.Message}";
                return response;
            }
        }

        public async Task<Response<List<Servicos>>> BuscarTodosServicos()
        {
            Response<List<Servicos>> response = new Response<List<Servicos>>();
            try
            {
                var servicos = await _context.Servicos.ToListAsync();
                if (servicos == null || servicos.Count == 0)
                {
                    response.Status = false;
                    response.Mensagem = "Nenhum serviço encontrado.";
                    return response;
                }
                response.Status = true;
                response.Mensagem = "Serviços encontrados com sucesso.";
                response.Dados = servicos;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = $"Erro: {ex.Message}";
                return response;
            }
        }

        public async Task<Response<Servicos>> DeletarServico(int idServico)
        {
            Response<Servicos> response = new Response<Servicos>();
            try             {
                var servico = await _context.Servicos.FindAsync(idServico);
                if (servico == null)
                {
                    response.Status = false;
                    response.Mensagem = "Serviço não encontrado.";
                    return response;
                }
                _context.Servicos.Remove(servico);
                await _context.SaveChangesAsync();
                response.Status = true;
                response.Mensagem = "Serviço deletado com sucesso.";
                return response;
            } catch(Exception ex)
            {
                response.Status = false;
                response.Mensagem = $"Erro: {ex.Message}";
                return response;
            }
        }
    }
}
