using GestVeicular.Models;

namespace GestVeicular.Services.ClienteService
{
    public interface IClienteInterface
    {
        Task<Response<Cliente>> AdicionarCliente(Cliente cliente);
        Task<Response<Cliente>> AtualizarCliente(Cliente cliente);
        Task<Response<Cliente>> DeletarCliente(int idCliente);
        Task<Response<Cliente>> Detalhes(int idCliente);
        Task<Response<List<Cliente>>> ListarClientes();
        Task<Response<Cliente>> BuscarClientePorId(int idCliente);

    }
}
