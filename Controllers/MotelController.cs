using Microsoft.AspNetCore.Mvc;
using MotelAPI.Data;
using MotelAPI.DTOs;
using MotelAPI.Entities;

namespace MotelAPI.Controllers
{
    [Route("api/motel")]
    [ApiController]
    public class MotelController : ControllerBase
    {
        private readonly MotelDbContext _context;

        public MotelController(MotelDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("cadastrar")]
        public IActionResult CadastrarMotel([FromBody] MotelDto dto)
        {
            if (dto == null)
                return BadRequest("Dados inválidos.");

            var tipoSuite = _context.TiposSuite.Find(dto.TipoSuiteId);
            if (tipoSuite == null)
                return NotFound("Tipo de suíte não encontrado.");

            var motel = new Motel
            {
                Nome = dto.Nome,
                TipoSuiteId = dto.TipoSuiteId,
                TipoSuite = tipoSuite,
            };

            _context.Moteis.Add(motel);
            _context.SaveChanges();

            return CreatedAtAction(nameof(CadastrarMotel), new { id = motel.Id }, motel);
        }
    }
}
