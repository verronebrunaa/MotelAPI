using MotelAPI.Data;
using MotelAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using MotelAPI.Models;

namespace MotelAPI.Services
{
    public class FinanceService
    {
        private readonly MotelDbContext _dbContext;

        public FinanceService(MotelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<decimal> CalcularValorReservaAsync(int reservaId)
        {
            var reserva = await _dbContext.Reservas
                .Include(r => r.Motel)
                .Include(r => r.TipoSuite)
                .FirstOrDefaultAsync(r => r.Id == reservaId);

            if (reserva == null)
            {
                throw new KeyNotFoundException("Reserva não encontrada.");
            }

            var duracaoHoras = (reserva.DataSaida - reserva.DataEntrada).TotalHours;
            return reserva.TipoSuite.PrecoPorHora * (decimal)duracaoHoras;
        }

        public async Task ProcessarPagamentoAsync(int reservaId, decimal valor)
        {
            var reserva = await _dbContext.Reservas.FindAsync(reservaId);
            if (reserva == null)
            {
                throw new KeyNotFoundException("Reserva não encontrada.");
            }

            reserva.ValorTotal = valor;
            _dbContext.Reservas.Update(reserva);
            await _dbContext.SaveChangesAsync();
        }
    }
}
