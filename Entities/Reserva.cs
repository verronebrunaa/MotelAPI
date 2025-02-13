namespace MotelAPI.Entities
{
    /// <summary>
    /// Representa uma reserva no sistema.
    /// </summary>
    public class Reserva
    {
        /// <summary>
        /// Identificador único da reserva.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Data de entrada na reserva.
        /// </summary>
        public DateTime DataEntrada { get; set; }

        /// <summary>
        /// Data de saída da reserva.
        /// </summary>
        public DateTime DataSaida { get; set; }

        /// <summary>
        /// Valor total da reserva.
        /// </summary>
        public decimal ValorTotal { get; set; }

        /// <summary>
        /// Identificador do usuário associado à reserva.
        /// </summary>
        public int UsuarioId { get; set; } // Alteração aqui

        /// <summary>
        /// Usuário associado à reserva.
        /// </summary>
        public required Usuario Usuario { get; set; }

        /// <summary>
        /// Identificador do tipo de suíte associado à reserva.
        /// </summary>
        public int TipoSuiteId { get; set; }

        /// <summary>
        /// Tipo de suíte associado à reserva.
        /// </summary>
        public required TipoSuite TipoSuite { get; set; }

        /// <summary>
        /// Identificador do motel associado à reserva.
        /// </summary>
        public int MotelId { get; set; }

        /// <summary>
        /// Motel associado à reserva.
        /// </summary>
        public required Motel Motel { get; set; }
    }
}
