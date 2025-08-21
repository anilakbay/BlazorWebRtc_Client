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
        private readonly AppDbContext _context;           // DB context
        private readonly IConfiguration _configuration;   // App ayarları

        public LoginHandler(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // Login isteğini işleyen metod
        public async Task<(bool Success, string Token)> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .SingleOrDefaultAsync(u => u.UserName == request.UserName, cancellationToken);

            // Kullanıcı yoksa veya şifre yanlışsa başarısız
            if (user == null || !VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt))
                return (false, string.Empty);

            // JWT token oluştur
            var token = GenerateJwtToken(user);
            return (true, token);
        }

        // JWT ayarlarını temsil eden model
        private class JwtSettings
        {
            public string SecretKey { get; set; }
            public string Issuer { get; set; }
            public string Audience { get; set; }
            public int ExpirationMinutes { get; set; }
        }

        // JWT token üretme metodu
        private string GenerateJwtToken(User user)
        {
            // appsettings.json'dan JWT ayarlarını çek
            var jwtSettings = new JwtSettings();
            _configuration.GetSection("JwtSettings").Bind(jwtSettings);

            // Token imzası için güvenlik anahtarı
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Token içeriği (claims)
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString())
            };

            // JWT oluştur
            var token = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(jwtSettings.ExpirationMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Şifre doğrulama metodu
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
