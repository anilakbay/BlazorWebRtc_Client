using BlazorWebRtc_Domain;
using BlazorWebRtc_Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlazorWebRtc_Application.Features.Commands.Account.Login
{
    // Kullanıcı giriş işlemlerini yöneten MediatR handler
    public class LoginHandler : IRequestHandler<LoginCommand, (bool Success, string Token)>
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public LoginHandler(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<(bool Success, string Token)> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .SingleOrDefaultAsync(u => u.UserName == request.UserName, cancellationToken);

            if (user == null || !VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt))
                return (false, string.Empty);

            var token = GenerateJwtToken(user);
            return (true, token);
        }

        // JWT token üretme metodu, Bind kullanmadan
        private string GenerateJwtToken(User user)
        {
            // appsettings.json'dan direkt değer alıyoruz
            string secretKey = _configuration["JwtSettings:SecretKey"] ?? "default-secret-key";
            string issuer = _configuration["JwtSettings:Issuer"] ?? "default-issuer";
            string audience = _configuration["JwtSettings:Audience"] ?? "default-audience";
            int expirationMinutes = int.Parse(_configuration["JwtSettings:ExpirationMinutes"] ?? "60");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            var saltBytes = Convert.FromBase64String(storedSalt);

            var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
            ));

            return hash == storedHash;
        }
    }
}
