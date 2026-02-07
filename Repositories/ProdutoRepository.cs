using ApiRmBoutique.Data;
using ApiRmBoutique.Models;
using ApiRmBoutique.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiRmBoutique.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Produto>> GetAllAsync()
        {
            return await _context.Produtos
                 .Where(p => p.Ativo)
                .Include(p => p.Categoria) // traz a categoria junto
                .ToListAsync();
        }

        public async Task<Produto?> GetByIdAsync(int id)
        {
            return await _context.Produtos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Produto>> GetByCategoriaAsync(int categoriaId)
        {
            return await _context.Produtos
               .Where(p => p.CategoriaId == categoriaId && p.Ativo)
                .Include(p => p.Categoria)
                .ToListAsync();
        }

        public async Task AddAsync(Produto produto)
        {
            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Produto produto)
        {
            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var produto = await GetByIdAsync(id);
            if (produto != null)
            {
                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<Produto>> GetAllWithInactiveAsync()
        {
            return await _context.Produtos
                .Include(p => p.Categoria)
                .ToListAsync();
        }

    }
}