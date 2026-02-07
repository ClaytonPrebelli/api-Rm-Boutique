namespace ApiRmBoutique.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        // relação 1:N → uma categoria pode ter vários produtos
        public ICollection<Produto> Produtos { get; set; } = new List<Produto>();
    }
}
