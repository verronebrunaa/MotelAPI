using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MotelAPI.Entities;
using MotelAPI.Data;

[Route("api/auth")]
[ApiController]

public class AuthController : ControllerBase
{
    private readonly MotelDbContext _context;
    private readonly IConfiguration _config;

    public AuthController(MotelDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] Usuario user)
    {
        user.SenhaHash = BCrypt.Net.BCrypt.HashPassword(user.SenhaHash);
        _context.Usuarios.Add(user);
        _context.SaveChanges();
        return Ok(new { message = "Usuário criado com sucesso!" });
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] Usuario user)
    {
        var dbUser = _context.Usuarios.FirstOrDefault(u => u.Email == user.Email);
        if (dbUser == null || !BCrypt.Net.BCrypt.Verify(user.SenhaHash, dbUser.SenhaHash))
            return Unauthorized("Credenciais inválidas!");

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, dbUser.Email) }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return Ok(new { token = tokenHandler.WriteToken(token) });
    }
}
