using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using MotelAPI.Data;
using MotelAPI.DTOs;
using MotelAPI.Entities;

namespace MotelAPI.Controllers
{
    [Route("api/suites")]
    [ApiController]
    public class TipoSuiteController : ControllerBase
    {
        private readonly MotelDbContext _context;

        public TipoSuiteController(MotelDbContext context)
        {
            _context = context;
        }

        [HttpPost("cadastrar")]
        public IActionResult CadastrarSuite([FromBody] TipoSuiteDto dto)
        {
            if (dto == null)
                return BadRequest("Dados inválidos.");

            var suite = new TipoSuite
            {
                Nome = dto.Nome,
                PrecoPorHora = dto.PrecoPorHora,
                Disponivel = dto.Disponivel,
                Quantidade = dto.Quantidade,
            };

            _context.TiposSuite.Add(suite);
            _context.SaveChanges();

            var resultado = new
            {
                suite.Id,
                suite.Nome,
                PrecoPorHora = suite.PrecoPorHora.ToString("F2", CultureInfo.InvariantCulture), // Formatação
                suite.Disponivel,
                suite.Quantidade,
            };

            return CreatedAtAction(nameof(CadastrarSuite), new { id = suite.Id }, resultado);
        }
    }
}
