using MotelAPI.Data;
using MotelAPI.DTOs;

namespace MotelAPI.DTOs
{
    public class ReservationDto
    {
        public int UsuarioId { get; set; }
        public int TipoSuiteId { get; set; }
        public int MotelId { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime DataSaida { get; set; }
    }
}
