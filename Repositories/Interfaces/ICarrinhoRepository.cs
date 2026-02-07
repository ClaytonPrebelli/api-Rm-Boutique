using ApiRmBoutique.Models;

namespace ApiRmBoutique.Repositories.Interfaces
{
    public interface ICarrinhoRepository
    {
        Task<IEnumerable<CarrinhoItem>> GetCarrinhoAsync(string userEmail);
        Task AddItemAsync(string userEmail, int produtoId, int quantidade);
        Task UpdateItemAsync(int itemId, int quantidade);
        Task RemoveItemAsync(int itemId);
        Task ClearCarrinhoAsync(string userEmail);
    }
}