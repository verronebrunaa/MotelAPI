using Microsoft.AspNetCore.Mvc;
using MotelAPI.Models;
using MotelAPI.Services;

namespace MotelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinanceController : ControllerBase
    {
        private readonly FinanceService _financeService;

        public FinanceController(FinanceService financeService)
        {
            _financeService = financeService;
        }

        [HttpGet("calcular/{reservaId}")]
        public async Task<IActionResult> CalcularValorReserva(int reservaId)
        {
            try
            {
                var valor = await _financeService.CalcularValorReservaAsync(reservaId);
                return Ok(valor);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("processar-pagamento")]
        public async Task<IActionResult> ProcessarPagamento([FromBody] PagamentoModel model)
        {
            try
            {
                await _financeService.ProcessarPagamentoAsync(model.ReservaId, model.Valor);
                return Ok("Pagamento processado com sucesso.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
