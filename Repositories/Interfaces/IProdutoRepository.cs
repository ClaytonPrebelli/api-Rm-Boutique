using ApiRmBoutique.Models;

namespace ApiRmBoutique.Repositories.Interfaces
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> GetAllAsync();
        Task<Produto?> GetByIdAsync(int id);
        Task<IEnumerable<Produto>> GetByCategoriaAsync(int categoriaId);
        Task AddAsync(Produto produto);
        Task UpdateAsync(Produto produto);
        Task DeleteAsync(int id);
        Task<IEnumerable<Produto>> GetAllWithInactiveAsync();
    }
}