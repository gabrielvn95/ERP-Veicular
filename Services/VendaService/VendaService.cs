using GestVeicular.Data;
using GestVeicular.Enums;
using GestVeicular.Models;
using Microsoft.EntityFrameworkCore;

namespace GestVeicular.Services.VendaService
{
    public class VendaService : IVendaInterface
    {
        private readonly ApplicationDbContext _context;

        public VendaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<Venda>> AtualizarStatusVenda(int idVenda, StatusServicos novoStatus)
        {
            Response<Venda> response = new Response<Venda>();
            try
            {
                var vendaExistente = await _context.Vendas.FindAsync(idVenda);
                if (vendaExistente == null)
                {
                    response.Status = false;
                    response.Mensagem = "Venda não encontrada.";
                    return response;
                }

          
                vendaExistente.Status = novoStatus;
                vendaExistente.DataUltimaAtualizacao = DateTime.Now; 

                _context.Vendas.Update(vendaExistente);
                await _context.SaveChangesAsync();

                response.Status = true;
                response.Mensagem = "Status da venda atualizado com sucesso.";
                response.Dados = vendaExistente;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = $"Erro: {ex.Message}";
                return response;
            }
        }


        public async Task<Response<Venda>> AtualizarVenda(Venda venda)
        {
            Response<Venda> response = new Response<Venda>();
            try
            {
                var vendaExistente = await _context.Vendas.FindAsync(venda.IdVenda);
                if (vendaExistente == null)
                {
                    response.Status = false;
                    response.Mensagem = "Venda não encontrada.";
                    return response;
                }
                vendaExistente.Cliente = venda.Cliente;
                vendaExistente.Veiculo = venda.Veiculo;
                vendaExistente.ValorDaVenda = venda.ValorDaVenda;
                _context.Update(vendaExistente);
                await _context.SaveChangesAsync();
                response.Status = true;
                response.Mensagem = "Venda atualizada com sucesso.";
                response.Dados = vendaExistente;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = $"Erro: {ex.Message}";
                return response;
            }
        }


        public async Task<Response<Venda>> CriarVenda(Venda venda)
        {
            Response<Venda> response = new Response<Venda>();
            try
            {
                if(venda.Cliente == null || venda.Veiculo == null)
                {
                    response.Status = false;
                    response.Mensagem = "Venda deve ter um cliente e um veículo.";
                    return response;
                }
                await _context.AddAsync(venda);
                await _context.SaveChangesAsync();
                response.Status = true;
                response.Mensagem = "Venda criada com sucesso.";
                response.Dados = venda;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = $"Erro: {ex.Message}";
                return response;
            }
        }

        public async Task<Response<Venda>> DeletarVenda(int idVenda)
        {
            Response<Venda> response = new Response<Venda>();
            try
            {
               var vendaEncontrada = await _context.Vendas.FindAsync(idVenda);
                if (vendaEncontrada == null)
                {
                    response.Status = false;
                    response.Mensagem = "Venda não encontrada.";
                    return response;
                }
                _context.Remove(vendaEncontrada);
                await _context.SaveChangesAsync();
                response.Status = true;
                response.Mensagem = "Venda deletada com sucesso.";
                return response;

            }

            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = $"Erro: {ex.Message}";
                return response;


            }
           }

        public async Task<Response<Venda>> Detalhes(int idVenda)
        {
            Response<Venda> response = new Response<Venda>();
            try
            {
                var venda = await _context.Vendas.FindAsync(idVenda);
                if (venda == null)
                {
                    response.Status = false;
                    response.Mensagem = "Venda não encontrada.";
                    return response;
                }
                response.Status = true;
                response.Mensagem = "Venda encontrada com sucesso.";
                response.Dados = venda;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = $"Erro: {ex.Message}";
                return response;
            }
        }

        public async Task<Response<List<Venda>>> ListarVendas()
        {
            Response<List<Venda>> response = new Response<List<Venda>>();
            try
            {
                var vendas = await _context.Vendas.ToListAsync();
                if (vendas == null || vendas.Count == 0)
                {
                    response.Status = false;
                    response.Mensagem = "Nenhuma venda encontrada.";
                    return response;
                }
                response.Status = true;
                response.Mensagem = "Vendas encontradas com sucesso.";
                response.Dados = vendas;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = $"Erro: {ex.Message}";
                return response;
            }
        }
    }
}
