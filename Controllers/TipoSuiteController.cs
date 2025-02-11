using Microsoft.AspNetCore.Mvc;
using MotelAPI.Entities;
using MotelAPI.DTOs;
using MotelAPI.Data;

[Route("api/suites")]
[ApiController]
public class TipoSuiteController : ControllerBase
{
    private readonly MotelDbContext  _context;

    public TipoSuiteController(MotelDbContext  context)
    {
        _context = context;
    }

    [HttpPost("cadastrar")]
    public IActionResult CadastrarSuite([FromBody] TipoSuiteDTO dto)
    {
        if (dto == null) return BadRequest("Dados inválidos.");

        var suite = new TipoSuite
        {
            Nome = dto.Nome,
            PrecoPorHora = dto.PrecoPorHora,
            Disponivel = dto.Disponivel,
            Quantidade = dto.Quantidade
        };

        _context.TiposSuite.Add(suite);
        _context.SaveChanges();

        return CreatedAtAction(nameof(CadastrarSuite), new { id = suite.Id }, suite);
    }
}
