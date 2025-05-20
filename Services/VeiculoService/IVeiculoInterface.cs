using GestVeicular.Models;

namespace GestVeicular.Services.VeiculoService
{
    public interface IVeiculoInterface
    {
        Task<Response<Veiculo>> AdicionarVeiculo(Veiculo veiculo);
        Task<Response<Veiculo>> AtualizarVeiculo(Veiculo veiculo);
        Task<Response<Veiculo>> DeletarVeiculo(int idVeiculo);
        Task<Response<Veiculo>> Detalhes(int idVeiculo);
        Task<Response<List<Veiculo>>> ListarVeiculos();
        Task<Response<Veiculo>> BuscarVeiculoPorId(int idVeiculo);
    

    }
}
