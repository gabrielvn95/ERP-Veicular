using GestVeicular.Models;

namespace GestVeicular.Services.ServicosService
{
    public interface IServicosInterface
    {
        Task<Response<Servicos>> AdicionarServico(Servicos servico);
        Task<Response<Servicos>> AtualizarServico(Servicos servico);
        Task<Response<Servicos>> BuscarServicoPorId(int idServico);
        Task<Response<List<Servicos>>> BuscarTodosServicos();
        Task<Response<Servicos>> DeletarServico(int idServico);
        Task<Response<Servicos>> BuscarServicosPorId(int idServico);

    }
}
