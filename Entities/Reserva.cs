namespace MotelAPI.Entities
{
    public class Reserva
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int TipoSuiteId { get; set; }
        public int MotelId { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime DataSaida { get; set; }
        public decimal ValorTotal { get; set; }

        // Relationships
        public Cliente? Cliente { get; set; }
        public TipoSuite? TipoSuite { get; set; }
        public Motel? Motel { get; set; }
    }
}
