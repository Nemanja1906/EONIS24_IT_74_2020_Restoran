using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Restoran.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Restoran.Data
{
    public class AutentifikacijaRepository
    {
        private readonly RestoranContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly HashingService _hashingService;

        public AutentifikacijaRepository(RestoranContext dbContext, IConfiguration configuration, HashingService hashingService)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _hashingService = hashingService;
        }

        public async Task<object> Authenticate(string username, string password)
        {
            var user = await _dbContext.Musterija.SingleOrDefaultAsync(x => x.KontaktMusterije == username);

            if (user != null && _hashingService.VerifyPassword(user.ImeMusterije, password))
            {
                // Remove sensitive data
                user.ImeMusterije = null;

                var token = GenerateJwtToken(user.MusterijaID, "User");
                return new { Token = token, Role = "User" };
            }

            var admin = await _dbContext.Zaposleni.SingleOrDefaultAsync(x => x.KontaktZaposlenog == username);

            if (admin != null && _hashingService.VerifyPassword(admin.ImeZaposlenog, password))
            {
                // Remove sensitive data
                admin.ImeZaposlenog = null;

                var token = GenerateJwtToken(admin.ZaposleniID, "Admin");
                return new { Token = token, Role = "Admin" };
            }

            return null;
        }

        private string GenerateJwtToken(Guid id, string role)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                new Claim(ClaimTypes.Role, role),
                // Add other claims if needed
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["ExpiresInMinutes"])),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }
    }
}
