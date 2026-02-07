using ApiRmBoutique.Data;
using ApiRmBoutique.Enums;
using ApiRmBoutique.Models;
using ApiRmBoutique.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiRmBoutique.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _context;

        public PedidoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pedido>> GetPedidosAsync(string userEmail)
        {
            return await _context.Pedidos
                .Include(p => p.Itens)
                .ThenInclude(i => i.Produto)
                .Where(p => p.UserEmail == userEmail)
                .ToListAsync();
        }

        public async Task<Pedido?> GetPedidoByIdAsync(int id)
        {
            return await _context.Pedidos
                .Include(p => p.Itens)
                .ThenInclude(i => i.Produto)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Pedido> CriarPedidoAsync(string userEmail)
        {
            var itensCarrinho = await _context.CarrinhoItens
                .Include(c => c.Produto)
                .Where(c => c.UserEmail == userEmail && c.Produto.Ativo)
                .ToListAsync();

            if (!itensCarrinho.Any())
                throw new Exception("Carrinho vazio ou produtos inativos");

            var pedido = new Pedido
            {
                UserEmail = userEmail,
                DataPedido = DateTime.UtcNow,
                ValorTotal = itensCarrinho.Sum(i => i.Produto.Preco * i.Quantidade),
                Status = StatusPedido.Pendente,
                StatusPagamento = StatusPagamento.Pendente,
                FormaPagamento = null,
                Itens = itensCarrinho.Select(i => new PedidoItem
                {
                    ProdutoId = i.ProdutoId,
                    Quantidade = i.Quantidade,
                    PrecoUnitario = i.Produto.Preco
                }).ToList()
            };

            _context.Pedidos.Add(pedido);
            _context.CarrinhoItens.RemoveRange(itensCarrinho); // limpa carrinho
            await _context.SaveChangesAsync();

            return pedido;
        }
        public async Task AtualizarStatusAsync(int id, StatusPedido novoStatus)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
                throw new Exception("Pedido não encontrado");

            pedido.Status = novoStatus;
            _context.Pedidos.Update(pedido);
            await _context.SaveChangesAsync();
        }
        public async Task AtualizarFormaPagamentoAsync(int pedidoId, FormaPagamento forma)
        {
            var pedido = await _context.Pedidos.FindAsync(pedidoId);
            if (pedido == null)
                throw new Exception("Pedido não encontrado");

            pedido.FormaPagamento = forma;
            _context.Pedidos.Update(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarStatusPagamentoAsync(int pedidoId, StatusPagamento status)
        {
            var pedido = await _context.Pedidos.FindAsync(pedidoId);
            if (pedido == null)
                throw new Exception("Pedido não encontrado");

            pedido.StatusPagamento = status;
            _context.Pedidos.Update(pedido);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Pedido>> GetPedidosPorStatusAsync(StatusPedido status)
        {
            return await _context.Pedidos
                .Include(p => p.Itens)
                .ThenInclude(i => i.Produto)
                .Where(p => p.Status == status)
                .ToListAsync();
        }

        public async Task<IEnumerable<Pedido>> GetPedidosPorPagamentoAsync(StatusPagamento status)
        {
            return await _context.Pedidos
                .Include(p => p.Itens)
                .ThenInclude(i => i.Produto)
                .Where(p => p.StatusPagamento == status)
                .ToListAsync();
        }
        public async Task<decimal> GetTotalVendasAsync(DateTime inicio, DateTime fim)
        {
            return await _context.Pedidos
                .Where(p => p.DataPedido >= inicio && p.DataPedido <= fim && p.StatusPagamento == StatusPagamento.Aprovado)
                .SumAsync(p => p.ValorTotal);
        }


    }
}