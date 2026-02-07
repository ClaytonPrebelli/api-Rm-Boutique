using ApiRmBoutique.Enums;
using ApiRmBoutique.Models;

namespace ApiRmBoutique.Repositories.Interfaces
{
    public interface IPedidoRepository
    {
        Task<IEnumerable<Pedido>> GetPedidosAsync(string userEmail);
        Task<Pedido?> GetPedidoByIdAsync(int id);
        Task<Pedido> CriarPedidoAsync(string userEmail);
        Task AtualizarStatusAsync(int id, StatusPedido novoStatus);
        Task AtualizarFormaPagamentoAsync(int pedidoId, FormaPagamento forma);
        Task AtualizarStatusPagamentoAsync(int pedidoId, StatusPagamento status);
        Task<IEnumerable<Pedido>> GetPedidosPorStatusAsync(StatusPedido status);
        Task<IEnumerable<Pedido>> GetPedidosPorPagamentoAsync(StatusPagamento status);
        Task<decimal> GetTotalVendasAsync(DateTime inicio, DateTime fim);

    }
}