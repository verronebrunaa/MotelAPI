using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotelAPI.Data;
using MotelAPI.Entities;

namespace MotelAPI.Controllers
{
    [Route("api/tiposuite-motel")]
    [ApiController]
    public class TipoSuiteMotelController : ControllerBase
    {
        private readonly MotelDbContext _context;

        public TipoSuiteMotelController(MotelDbContext context)
        {
            _context = context;
        }

        [HttpGet("tiposuite/{id}")]
        public async Task<IActionResult> GetTipoSuite(int id)
        {
            var tipoSuite = await _context.TiposSuite.FirstOrDefaultAsync(t => t.Id == id);
            if (tipoSuite == null)
                return NotFound("Tipo de suíte não encontrado.");

            return Ok(tipoSuite);
        }

        [HttpGet("motel/{id}")]
        public async Task<IActionResult> GetMotel(int id)
        {
            var motel = await _context.Moteis.FirstOrDefaultAsync(m => m.Id == id);
            if (motel == null)
                return NotFound("Motel não encontrado.");

            return Ok(motel);
        }
    }
}
