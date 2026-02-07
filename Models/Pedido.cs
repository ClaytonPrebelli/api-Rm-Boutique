using ApiRmBoutique.Enums;

namespace ApiRmBoutique.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public string UserEmail { get; set; } = string.Empty;
        public DateTime DataPedido { get; set; } = DateTime.UtcNow;
        public decimal ValorTotal { get; set; }
        public StatusPedido Status { get; set; } = StatusPedido.Pendente;
        public StatusPagamento StatusPagamento { get; set; } = StatusPagamento.Pendente;
        public FormaPagamento? FormaPagamento { get; set; }

        public ICollection<PedidoItem> Itens { get; set; } = new List<PedidoItem>();
    }

}
