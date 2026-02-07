using ApiRmBoutique.Enums;
using ApiRmBoutique.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiRmBoutique.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // só usuários logados podem criar/ver pedidos
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidosController(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetPedidos(string userEmail)
        {
            var pedidos = await _pedidoRepository.GetPedidosAsync(userEmail);
            return Ok(pedidos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPedido(int id)
        {
            var pedido = await _pedidoRepository.GetPedidoByIdAsync(id);
            if (pedido == null)
                return NotFound();

            return Ok(pedido);
        }

        [HttpPost("finalizar")]
        public async Task<IActionResult> CriarPedido(string userEmail)
        {
            var pedido = await _pedidoRepository.CriarPedidoAsync(userEmail);
            return Ok(pedido);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> AtualizarStatus(int id, StatusPedido novoStatus)
        {
            try
            {
                await _pedidoRepository.AtualizarStatusAsync(id, novoStatus);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPatch("{id}/forma")]

        public async Task<IActionResult> AtualizarFormaPagamento(int id, FormaPagamento forma)
        {
            try
            {
                await _pedidoRepository.AtualizarFormaPagamentoAsync(id, forma);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPatch("{id}/pagamento")]
        public async Task<IActionResult> AtualizarStatusPagamento(int id, StatusPagamento status)
        {
            try
            {
                await _pedidoRepository.AtualizarStatusPagamentoAsync(id, status);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetPedidosPorStatus(StatusPedido status)
        {
            var pedidos = await _pedidoRepository.GetPedidosPorStatusAsync(status);
            return Ok(pedidos);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("pagamento/{status}")]
        public async Task<IActionResult> GetPedidosPorPagamento(StatusPagamento status)
        {
            var pedidos = await _pedidoRepository.GetPedidosPorPagamentoAsync(status);
            return Ok(pedidos);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("vendas")]
        public async Task<IActionResult> GetTotalVendas(DateTime inicio, DateTime fim)
        {
            var total = await _pedidoRepository.GetTotalVendasAsync(inicio, fim);
            return Ok(total);
        }


    }
}