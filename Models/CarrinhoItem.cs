namespace ApiRmBoutique.Models
{
    public class CarrinhoItem
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public string UserEmail { get; set; } = string.Empty; // identifica o dono do carrinho
    }
}