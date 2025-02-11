using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MotelAPI.Data;
using MotelAPI.Entities;
using MotelAPI.Models;

namespace MotelAPI.Controllers
{
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

        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(typeof(IActionResult), 400)]
        [HttpPost("register")]
        public IActionResult Register([FromBody] Usuario user)
        {
            var existingUser = _context.Usuarios.FirstOrDefault(u => u.Email == user.Email);
            if (existingUser != null)
            {
                return BadRequest(new { message = "Email já está em uso!" });
            }

            user.Senha = BCrypt.Net.BCrypt.HashPassword(user.Senha);

            _context.Usuarios.Add(user);
            _context.SaveChanges();

            return Ok(new { message = "Usuário criado com sucesso!" });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel user)
        {
            if (
                user == null
                || string.IsNullOrEmpty(user.Email)
                || string.IsNullOrEmpty(user.Senha)
            )
            {
                return BadRequest(new { message = "Email e senha são obrigatórios." });
            }

            var dbUser = _context.Usuarios.FirstOrDefault(u => u.Email == user.Email);

            if (dbUser == null || !BCrypt.Net.BCrypt.Verify(user.Senha, dbUser.Senha))
            {
                return Unauthorized(new { message = "Credenciais inválidas!" });
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("JwtSettings:SecretKey");

            if (key == null || key.Length == 0)
            {
                return StatusCode(500, new { message = "Chave JWT não configurada corretamente." });
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, dbUser.Id.ToString()),
                        new Claim(ClaimTypes.Name, dbUser.Email),
                    }
                ),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                ),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new { token = tokenHandler.WriteToken(token) });
        }
    }
}
