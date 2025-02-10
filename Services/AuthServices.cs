using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MotelAPI.Entities;
using MotelAPI.Models;
using MotelAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MotelAPI.Configurations;

namespace MotelAPI.Services
{
    public class AuthService
    {
        private readonly MotelDbContext _dbContext;
        private readonly JwtSettings _jwtSettings;
        private readonly PasswordHasher<Usuario> _passwordHasher;

        public AuthService(MotelDbContext dbContext, IOptions<JwtSettings> jwtSettings)
        {
            _dbContext = dbContext;
            _jwtSettings = jwtSettings.Value;
            _passwordHasher = new PasswordHasher<Usuario>();
        }

        public async Task<string> LoginAsync(LoginModel loginModel)
        {
            var usuario = await _dbContext.Usuarios
                .FirstOrDefaultAsync(u => u.Email == loginModel.Email);

            if (usuario == null || !VerifyPassword(usuario, loginModel.Senha))
            {
                throw new UnauthorizedAccessException("Credenciais inválidas");
            }

            return GenerateJwtToken(usuario);
        }

        private bool VerifyPassword(Usuario usuario, string senha)
        {
            var result = _passwordHasher.VerifyHashedPassword(usuario, usuario.SenhaHash, senha);
            return result == PasswordVerificationResult.Success;
        }

        private string GenerateJwtToken(Usuario usuario)
        {
            var claims = new[] 
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Email, usuario.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddHours(_jwtSettings.ExpiryDurationInHours),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
