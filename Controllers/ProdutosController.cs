using ApiRmBoutique.Models;
using ApiRmBoutique.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiRmBoutique.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutosController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        // GET: api/produtos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
        {
            var produtos = await _produtoRepository.GetAllAsync();
            return Ok(produtos);
        }
        // GET: api/produtos/inativos → todos (ativos + inativos) [somente Admin]
        [Authorize(Roles = "Admin")]
        [HttpGet("inativos")]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutosComInativos()
        {
            var produtos = await _produtoRepository.GetAllWithInactiveAsync();
            return Ok(produtos);
        }

        // GET: api/produtos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetProduto(int id)
        {
            var produto = await _produtoRepository.GetByIdAsync(id);
            if (produto == null)
                return NotFound();

            return Ok(produto);
        }

        // GET: api/produtos/categoria/3
        [HttpGet("categoria/{categoriaId}")]
        public async Task<ActionResult<IEnumerable<Produto>>> GetByCategoria(int categoriaId)
        {
            var produtos = await _produtoRepository.GetByCategoriaAsync(categoriaId);
            return Ok(produtos);
        }

        // POST: api/produtos
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Produto>> PostProduto(Produto produto)
        {
            await _produtoRepository.AddAsync(produto);
            return CreatedAtAction(nameof(GetProduto), new { id = produto.Id }, produto);
        }

        // PUT: api/produtos/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduto(int id, Produto produto)
        {
            if (id != produto.Id)
                return BadRequest();

            await _produtoRepository.UpdateAsync(produto);
            return NoContent();
        }

        // DELETE: api/produtos/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            await _produtoRepository.DeleteAsync(id);
            return NoContent();
        }
        // PATCH: api/produtos/{id}/desativar → desativa produto
        [Authorize(Roles = "Admin")]
        [HttpPatch("{id}/desativar")]
        public async Task<IActionResult> DesativarProduto(int id)
        {
            var produto = await _produtoRepository.GetByIdAsync(id);
            if (produto == null)
                return NotFound();

            produto.Ativo = false;
            await _produtoRepository.UpdateAsync(produto);

            return NoContent();
        }
        // PATCH: api/produtos/{id}/ativar → reativa produto
        [Authorize(Roles = "Admin")]
        [HttpPatch("{id}/ativar")]
        public async Task<IActionResult> AtivarProduto(int id)
        {
            var produto = await _produtoRepository.GetByIdAsync(id);
            if (produto == null)
                return NotFound();

            produto.Ativo = true;
            await _produtoRepository.UpdateAsync(produto);

            return NoContent();
        }


    }
}