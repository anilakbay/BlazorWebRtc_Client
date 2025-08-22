using BlazorWebRtc_Application.Features.Commands.Account.Register;
using BlazorWebRtc_Domain;
using BlazorWebRtc_Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore; // AnyAsync için
using System.Security.Cryptography;

namespace BlazorWebRtc_Application.Features.Commands.Account.Register
{
    // Kullanıcı kayıt işlemlerini yöneten MediatR handler
    public class RegisterHandler : IRequestHandler<RegisterCommand, Guid>
    {
        private readonly AppDbContext _context;

        public RegisterHandler(AppDbContext context)
        {
            _context = context;
        }

        // Register isteğini işleyen metod
        public async Task<Guid> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            // Kullanıcı adı daha önce varsa hata fırlat
            if (await _context.Users.AnyAsync(u => u.UserName == request.UserName, cancellationToken))
                throw new Exception("User already exist");

            // Şifreyi hash ve salt ile sakla
            var (passwordHash, salt) = HashPassword(request.Password);

            var user = new User()
            {
                UserName = request.UserName,
                PasswordHash = passwordHash,
                PasswordSalt = salt,
                Email = request.Email,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            // Profil resmi varsa kaydet
            if (request.ProfilePicture is not null && request.ProfilePicture.Length > 0)
            {
                var imagePath = await SaveProfilePicture(request.ProfilePicture, user.Id);
                user.ProfilePicture = imagePath;
                _context.Users.Update(user);
                await _context.SaveChangesAsync(cancellationToken);
            }

            // Kullanıcı ID'si her durumda dön
            return user.Id;
        }

        // Profil resmi kaydetme metodu
        private async Task<string> SaveProfilePicture(IFormFile profilePicture, Guid userId)
        {
            var uploadsFolder = Path.Combine("wwwroot", "images", "profile_pictures");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{userId}_{DateTime.UtcNow.Ticks}{Path.GetExtension(profilePicture.FileName)}";
            var filePath = Path.Combine(uploadsFolder, fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
                await profilePicture.CopyToAsync(fileStream);

            return Path.Combine("images", "profile_pictures", fileName);
        }

        // Şifre hash ve salt üretme metodu
        private (string hash, string salt) HashPassword(string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(salt);

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            string saltString = Convert.ToBase64String(salt);
            return (hashed, saltString);
        }
    }
}
