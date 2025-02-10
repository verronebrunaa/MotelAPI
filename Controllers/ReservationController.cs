using Microsoft.AspNetCore.Mvc;
using MotelAPI.Services;
using MotelAPI.Models;
using System.Threading.Tasks;

namespace MotelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationService _reservationService;

        public ReservationController(ReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost("criar")]
        public async Task<IActionResult> CriarReserva([FromBody] ReservaModel reservaModel)
        {
            try
            {
                var reserva = await _reservationService.CriarReservaAsync(reservaModel);
                return CreatedAtAction(nameof(CriarReserva), new { reservaId = reserva.Id }, reserva);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("atualizar/{reservaId}")]
        public async Task<IActionResult> AtualizarReserva(int reservaId, [FromBody] ReservaModel reservaModel)
        {
            try
            {
                var reserva = await _reservationService.AtualizarReservaAsync(reservaId, reservaModel);
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
