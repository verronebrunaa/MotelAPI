namespace MotelAPI.Models
{
    public class ReservaModel
    {
        public int UsuarioId { get; set; }
        public int TipoSuiteId { get; set; }
        public int MotelId { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime DataSaida { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
