namespace MotelAPI.Entities
{
    public class Reserva
    {
        public int Id { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime DataSaida { get; set; }
        public decimal ValorTotal { get; set; }

        // Relacionamento com a tabela de clientes
        public int UsuarioId { get; set; } // Alteração aqui
        public Usuario Usuario { get; set; }

        // Relacionamento com a tabela de tipo de suíte
        public int TipoSuiteId { get; set; }
        public TipoSuite TipoSuite { get; set; }

        // Relacionamento com a tabela de motel
        public int MotelId { get; set; }
        public Motel Motel { get; set; }
    }
}
