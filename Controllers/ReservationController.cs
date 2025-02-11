using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotelAPI.Data;
using MotelAPI.DTOs;
using MotelAPI.Entities;
using MotelAPI.Models;
using MotelAPI.Services;

namespace MotelAPI.Controllers
{
    [Route("api/reservas")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        private readonly MotelDbContext _context;
        private readonly ReservationService _reservationService;

        public ReservaController(MotelDbContext context, ReservationService reservationService)
        {
            _context = context;
            _reservationService = reservationService;
        }

        [HttpPost("criar")]
        public async Task<IActionResult> CriarReserva([FromBody] ReservationDto dto)
        {
            if (dto == null)
                return BadRequest("Dados inválidos.");

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == dto.UsuarioId);
            if (usuario == null)
                return BadRequest("Usuário não encontrado.");

            if (string.IsNullOrEmpty(usuario.Documento))
                return BadRequest("Documento não encontrado ou não informado.");

            var tipoSuite = await _context.TiposSuite.FirstOrDefaultAsync(t =>
                t.Id == dto.TipoSuiteId && t.Quantidade > 0 && t.Disponivel && t.PrecoPorHora > 0
            );
            if (tipoSuite == null)
                return BadRequest("Tipo de suíte não encontrado.");

            var moteis = await _context.Moteis.FirstOrDefaultAsync(m => m.Id == dto.MotelId);
            if (moteis == null)
                return BadRequest("Motel não encontrado.");

            var reserva = new Reserva
            {
                DataEntrada = dto.DataEntrada,
                DataSaida = dto.DataSaida,
                UsuarioId = dto.UsuarioId,
                TipoSuiteId = dto.TipoSuiteId,
                MotelId = dto.MotelId,
            };

            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CriarReserva), new { id = reserva.Id }, reserva);
        }

        [HttpPut("atualizar/{reservaId}")]
        public async Task<IActionResult> AtualizarReserva(
            int reservaId,
            [FromBody] ReservaModel reservaModel
        )
        {
            try
            {
                var reserva = await _reservationService.AtualizarReservaAsync(
                    reservaId,
                    reservaModel
                );
                return Ok(reserva);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("excluir/{reservaId}")]
        public async Task<IActionResult> ExcluirReserva(int reservaId)
        {
            try
            {
                await _reservationService.ExcluirReservaAsync(reservaId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
