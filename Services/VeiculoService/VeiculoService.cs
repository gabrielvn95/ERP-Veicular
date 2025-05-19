using GestVeicular.Data;
using GestVeicular.Models;
using Microsoft.EntityFrameworkCore;

namespace GestVeicular.Services.VeiculoService
{
    public class VeiculoService : IVeiculoInterface
    {
        private readonly ApplicationDbContext _context;
        public VeiculoService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Response<Veiculo>> AdicionarVeiculo(Veiculo veiculo)
        {
            Response<Veiculo> response = new Response<Veiculo>();
            try
            {
                await _context.AddAsync(veiculo);
                await _context.SaveChangesAsync();
                response.Status = true;
                response.Mensagem = "Veículo adicionado com sucesso.";
                response.Dados = veiculo;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = $"Erro: {ex.Message}";
                return response;
            }
        }

        public async Task<Response<Veiculo>> AtualizarVeiculo(Veiculo veiculo)
        {
            Response<Veiculo> response = new Response<Veiculo>();
            try { 
                _context.Update(veiculo);
                await _context.SaveChangesAsync();
                response.Status = true;
                response.Mensagem = "Veículo atualizado com sucesso.";
                response.Dados = veiculo;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = $"Erro: {ex.Message}";
                return response;
            }
        }

        public async Task<Response<Veiculo>> BuscarVeiculoPorId(int idVeiculo)
        {
            Response<Veiculo> response = new Response<Veiculo>();
            try
            {
                var veiculo = await _context.Veiculos.FindAsync(idVeiculo);
                if (veiculo == null)
                {
                    response.Status = false;
                    response.Mensagem = "Veículo não encontrado.";
                    return response;
                }
                response.Status = true;
                response.Mensagem = "Veículo encontrado com sucesso.";
                response.Dados = veiculo;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = $"Erro: {ex.Message}";
                return response;
            }
        }

        public async Task<Response<Veiculo>> DeletarVeiculo(int idVeiculo)
        {
            Response<Veiculo> response = new Response<Veiculo>();
            try
            {
                var veiculo = await _context.Veiculos.FindAsync(idVeiculo);
                if (veiculo == null)
                {
                    response.Status = false;
                    response.Mensagem = "Veículo não encontrado.";
                    return response;
                }
                _context.Veiculos.Remove(veiculo);
                await _context.SaveChangesAsync();
                response.Status = true;
                response.Mensagem = "Veículo deletado com sucesso.";
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = $"Erro: {ex.Message}";
                return response;
            }

        }

        public async Task<Response<Veiculo>> Detalhes(int idVeiculo)
        {
            Response<Veiculo> response = new Response<Veiculo>();
            try
            {
                var veiculo = await _context.Veiculos.FindAsync(idVeiculo);
                if (veiculo == null)
                {
                    response.Status = false;
                    response.Mensagem = "Veículo não encontrado.";
                    return response;
                }
                response.Status = true;
                response.Mensagem = "Veículo encontrado com sucesso.";
                response.Dados = veiculo;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = $"Erro: {ex.Message}";
                return response;
            }
        }

        public async Task<Response<List<Veiculo>>> ListarVeiculos()
        {
            Response<List<Veiculo>> response = new Response<List<Veiculo>>();
            try             {
                var veiculos = await _context.Veiculos.ToListAsync();
                if (veiculos == null || !veiculos.Any())
                {
                    response.Status = false;
                    response.Mensagem = "Nenhum veículo encontrado.";
                    return response;
                }
                response.Status = true;
                response.Mensagem = "Veículos encontrados com sucesso.";
                response.Dados = veiculos;
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
