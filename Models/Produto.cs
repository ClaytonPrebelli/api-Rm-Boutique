namespace ApiRmBoutique.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public int Estoque { get; set; }

        // chave estrangeira
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        public bool Ativo { get; set; } = true; // flag de ativo

    }
}