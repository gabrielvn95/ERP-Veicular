using GestVeicular.Enums;
using GestVeicular.Models;

namespace GestVeicular.Services.VendaService
{
    public interface IVendaInterface
    {
        Task<Response<Venda>> CriarVenda(Venda venda);
        Task<Response<Venda>> AtualizarVenda(Venda venda);
        Task<Response<Venda>> DeletarVenda(int idVenda);
        Task<Response<Venda>> Detalhes(int idVenda);
        Task<Response<List<Venda>>> ListarVendas();
        Task<Response<Venda>> AtualizarStatusVenda(int idVenda, StatusServicos novoStatus);

    }
}
