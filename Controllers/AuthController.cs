using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MotelAPI.Entities;
using MotelAPI.Data;
using BCrypt.Net;

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

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="user">The user to be registered.</param>
        /// <response code="200">If the user was created successfully.</response>
        /// <response code="400">If the email is already in use.</response>
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(typeof(IActionResult), 400)]
        [HttpPost("register")]
        public IActionResult Register([FromBody] Usuario user)
        {
            // Verificar se o email já existe
            var existingUser = _context.Usuarios.FirstOrDefault(u => u.Email == user.Email);
            if (existingUser != null)
            {
                return BadRequest(new { message = "Email já está em uso!" });
            }

            // Criptografar a senha antes de salvar
            user.SenhaHash = BCrypt.Net.BCrypt.HashPassword(user.Senha);

            user.Senha = null;  // Garantir que a senha não será salva como texto claro

            // Salvar o usuário no banco de dados
            _context.Usuarios.Add(user);
            _context.SaveChanges();

            return Ok(new { message = "Usuário criado com sucesso!" });
        }

        // Método de login
        [HttpPost("login")]
        public IActionResult Login([FromBody] Usuario user)
        {
            // Verificar se o usuário existe no banco de dados
            var dbUser = _context.Usuarios.FirstOrDefault(u => u.Email == user.Email);
            if (dbUser == null || !BCrypt.Net.BCrypt.Verify(user.Senha, dbUser.SenhaHash))
            {
                return Unauthorized(new { message = "Credenciais inválidas!" });
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, dbUser.Id.ToString()),  // Usar o ID como identificador
                new Claim(ClaimTypes.Name, dbUser.Email)  // Adicionar o email como claim
            }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new { token = tokenHandler.WriteToken(token) });
        }
    }
}