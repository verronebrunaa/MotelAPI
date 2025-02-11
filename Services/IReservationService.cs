using System.Threading.Tasks;
using MotelAPI.Entities;
using MotelAPI.Models;

namespace MotelAPI.Services
{
    public interface IReservationService
    {
        Task<Reserva> CriarReservaAsync(ReservaModel reservaModel);
        Task<Reserva> AtualizarReservaAsync(int reservaId, ReservaModel reservaModel);
        Task ExcluirReservaAsync(int reservaId);
    }
}
