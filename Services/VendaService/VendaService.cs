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
            var response = new Response<Venda>();
            try
            {
                var vendaExistente = await _context.Vendas
                    .Include(s => s.Cliente)
                    .Include(s => s.Veiculo)
                    .FirstOrDefaultAsync(s => s.IdVenda == idVenda);

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
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = $"Erro: {ex.Message}";
            }
            return response;
        }


        public async Task<Response<Venda>> AtualizarVenda(Venda venda)
        {
            Response<Venda> response = new Response<Venda>();
            try
            {
                _context.Vendas.Update(venda);
                await _context.SaveChangesAsync();
                response.Status = true;
                response.Mensagem = "Venda atualizada com sucesso.";
                response.Dados = venda;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = $"Erro: {ex.Message}";
            }
            return response;
        }

      
        public async Task<Response<Venda>> AdicionarVenda(Venda venda)
        {
            var response = new Response<Venda>();
            try
            {
                await _context.AddAsync(venda);
                await _context.SaveChangesAsync();
                response.Status = true;
                response.Mensagem = "Venda adicionado com sucesso.";
                response.Dados = venda;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = $"Erro: {ex.Message}";
            }
            return response;
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
            var response = new Response<List<Venda>>();
            try
            {
                var venda = await _context.Vendas
                    .Include(s => s.Cliente)
                    .Include(s => s.Veiculo)
                    .ToListAsync();

                if (venda == null || venda.Count == 0)
                {
                    response.Status = false;
                    response.Mensagem = "Nenhuma venda encontrada.";
                    return response;
                }
                response.Status = true;
                response.Mensagem = "Vendas encontradas com sucesso.";
                response.Dados = venda;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = $"Erro: {ex.Message}";
            }
            return response;
        }

        public async Task<Response<Venda>> BuscarVendaPorId(int idVenda)
        {
            var response = new Response<Venda>();
            try
            {
                var venda = await _context.Vendas
                    .Include(s => s.Cliente)
                    .Include(s => s.Veiculo)
                    .FirstOrDefaultAsync(s => s.IdVenda == idVenda);

                if (venda == null)
                {
                    response.Status = false;
                    response.Mensagem = "Vendas não encontrada.";
                    return response;
                }
                response.Status = true;
                response.Mensagem = "Vendas encontrada com sucesso.";
                response.Dados = venda;
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
