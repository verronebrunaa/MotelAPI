using MotelAPI.Data;
using MotelAPI.Entities;
using MotelAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MotelAPI.Services
{
    public class ReservationService
    {
        private readonly MotelDbContext _dbContext;

        public ReservationService(MotelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Reserva> CriarReservaAsync(ReservaModel reservaModel)
        {
            var cliente = await _dbContext.Clientes.FindAsync(reservaModel.ClienteId);
            var tipoSuite = await _dbContext.TiposSuite.FindAsync(reservaModel.TipoSuiteId);
            var motel = await _dbContext.Moteis.FindAsync(reservaModel.MotelId);

            if (cliente == null || tipoSuite == null || motel == null)
            {
                throw new KeyNotFoundException("Cliente, tipo de suíte ou motel não encontrado.");
            }

            var reservaExistente = await _dbContext.Reservas
                .Where(r => r.TipoSuiteId == reservaModel.TipoSuiteId &&
                            r.MotelId == reservaModel.MotelId &&

                                (r.DataEntrada < reservaModel.DataSaida && r.DataSaida > reservaModel.DataEntrada)
                            )
                .AnyAsync();

            if (reservaExistente)
            {
                throw new InvalidOperationException("A suíte já está reservada no período solicitado.");
            }

            var reserva = new Reserva
            {
                ClienteId = reservaModel.ClienteId,
                TipoSuiteId = reservaModel.TipoSuiteId,
                MotelId = reservaModel.MotelId,
                DataEntrada = reservaModel.DataEntrada,
                DataSaida = reservaModel.DataSaida,
                ValorTotal = reservaModel.ValorTotal
            };

            _dbContext.Reservas.Add(reserva);
            await _dbContext.SaveChangesAsync();

            return reserva;
        }

        public async Task<Reserva> AtualizarReservaAsync(int reservaId, ReservaModel reservaModel)
        {
            var reserva = await _dbContext.Reservas.FindAsync(reservaId);
            if (reserva == null)
            {
                throw new KeyNotFoundException("Reserva não encontrada.");
            }

            var reservaExistente = await _dbContext.Reservas
                .Where(r => r.TipoSuiteId == reservaModel.TipoSuiteId &&
                            r.MotelId == reservaModel.MotelId &&
                            r.Id != reservaId &&
                                (r.DataEntrada < reservaModel.DataSaida && r.DataSaida > reservaModel.DataEntrada)
                            )
                .AnyAsync();

            if (reservaExistente)
            {
                throw new InvalidOperationException("A suíte já está reservada no novo período solicitado.");
            }

            reserva.DataEntrada = reservaModel.DataEntrada;
            reserva.DataSaida = reservaModel.DataSaida;
            reserva.ValorTotal = reservaModel.ValorTotal;

            _dbContext.Reservas.Update(reserva);
            await _dbContext.SaveChangesAsync();

            return reserva;
        }

        public async Task ExcluirReservaAsync(int reservaId)
        {
            var reserva = await _dbContext.Reservas.FindAsync(reservaId);
            if (reserva == null)
            {
                throw new KeyNotFoundException("Reserva não encontrada.");
            }

            _dbContext.Reservas.Remove(reserva);
            await _dbContext.SaveChangesAsync();
        }
    }
}
