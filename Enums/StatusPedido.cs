namespace ApiRmBoutique.Enums
{
    public enum StatusPedido
    {
        Pendente,     // pedido criado, aguardando pagamento
        Pago,         // pagamento confirmado
        EmPreparacao, // separando produtos
        Enviado,      // saiu para entrega
        Entregue,     // entregue ao cliente
        Cancelado     // cancelado
    }
}
