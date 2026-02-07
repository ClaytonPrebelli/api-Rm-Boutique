namespace ApiRmBoutique.Enums
{
        public enum StatusPagamento
        {
            Pendente,   // aguardando pagamento
            Aprovado,   // pagamento confirmado
            Recusado,   // pagamento não autorizado
            Estornado   // devolução do valor
        }
}
