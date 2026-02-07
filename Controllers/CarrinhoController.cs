using ApiRmBoutique.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiRmBoutique.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // só usuários logados podem acessar o carrinho
    public class CarrinhoController : ControllerBase
    {
        private readonly ICarrinhoRepository _carrinhoRepository;

        public CarrinhoController(ICarrinhoRepository carrinhoRepository)
        {
            _carrinhoRepository = carrinhoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCarrinho(string userEmail)
        {
            var itens = await _carrinhoRepository.GetCarrinhoAsync(userEmail);
            return Ok(itens);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddItem(string userEmail, int produtoId, int quantidade)
        {
            await _carrinhoRepository.AddItemAsync(userEmail, produtoId, quantidade);
            return Ok();
        }

        [HttpPut("update/{itemId}")]
        public async Task<IActionResult> UpdateItem(int itemId, int quantidade)
        {
            await _carrinhoRepository.UpdateItemAsync(itemId, quantidade);
            return NoContent();
        }

        [HttpDelete("remove/{itemId}")]
        public async Task<IActionResult> RemoveItem(int itemId)
        {
            await _carrinhoRepository.RemoveItemAsync(itemId);
            return NoContent();
        }

        [HttpDelete("clear")]
        public async Task<IActionResult> ClearCarrinho(string userEmail)
        {
            await _carrinhoRepository.ClearCarrinhoAsync(userEmail);
            return NoContent();
        }
    }
}