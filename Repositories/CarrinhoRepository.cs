using ApiRmBoutique.Data;
using ApiRmBoutique.Models;
using ApiRmBoutique.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiRmBoutique.Repositories
{
    public class CarrinhoRepository : ICarrinhoRepository
    {
        private readonly AppDbContext _context;

        public CarrinhoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CarrinhoItem>> GetCarrinhoAsync(string userEmail)
        {
            return await _context.CarrinhoItens
                .Include(c => c.Produto)
                .Where(c => c.UserEmail == userEmail && c.Produto.Ativo) // só produtos ativos
                .ToListAsync();
        }

        public async Task AddItemAsync(string userEmail, int produtoId, int quantidade)
        {
            var produto = await _context.Produtos.FindAsync(produtoId);
            if (produto == null || !produto.Ativo)
                throw new Exception("Produto inválido ou inativo");

            var item = new CarrinhoItem
            {
                ProdutoId = produtoId,
                Quantidade = quantidade,
                UserEmail = userEmail
            };

            await _context.CarrinhoItens.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateItemAsync(int itemId, int quantidade)
        {
            var item = await _context.CarrinhoItens.FindAsync(itemId);
            if (item != null)
            {
                item.Quantidade = quantidade;
                _context.CarrinhoItens.Update(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveItemAsync(int itemId)
        {
            var item = await _context.CarrinhoItens.FindAsync(itemId);
            if (item != null)
            {
                _context.CarrinhoItens.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ClearCarrinhoAsync(string userEmail)
        {
            var itens = _context.CarrinhoItens.Where(c => c.UserEmail == userEmail);
            _context.CarrinhoItens.RemoveRange(itens);
            await _context.SaveChangesAsync();
        }
    }
}