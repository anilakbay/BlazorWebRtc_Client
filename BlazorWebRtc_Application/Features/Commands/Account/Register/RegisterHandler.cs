using BlazorWebRtc_Application.Models;
using BlazorWebRtc_Domain;
using BlazorWebRtc_Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace BlazorWebRtc_Application.Features.Commands.Account.Register
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, BaseResponseModel>
    {
        private readonly AppDbContext _context;
        private readonly BaseResponseModel _responseModel;


        public RegisterHandler(AppDbContext context, BaseResponseModel responseModel)
        {
            _context = context;
            _responseModel = responseModel;
        }
        public async Task<BaseResponseModel> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (await _context.Users.AnyAsync(u => u.UserName == request.UserName, cancellationToken))
            {
                _responseModel.IsSuccess = false;
                _responseModel.Message = "Username already exists.";
                return _responseModel;

            }

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

            if (request.ProfilePicture is not null && request.ProfilePicture.Length > 0)
            {
                var imagePath = await SaveProfilePicture(request.ProfilePicture, user.Id);
                user.ProfilePicture = imagePath;
                _context.Users.Update(user);
                await _context.SaveChangesAsync(cancellationToken);
                _responseModel.IsSuccess = true;
                _responseModel.Message = "User Created Successfully";
                _responseModel.Data = user;
                return _responseModel;

            }

            _responseModel.IsSuccess = false;
            _responseModel.Message = "Profile Picture Not Saved";
            return _responseModel;


        }

        private async Task<string> SaveProfilePicture(IFormFile profilePicture, Guid userId)
        {
            var uploadsFolder = Path.Combine("wwwroot", "images", "profile_pictures");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName = $"{userId}_{DateTime.UtcNow.Ticks}{Path.GetExtension(profilePicture.FileName)}";
            var filePath = Path.Combine(uploadsFolder, fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await profilePicture.CopyToAsync(fileStream);
            }
            return Path.Combine("images", "profile_pictures");
        }

        private (string hash, string salt) HashPassword(string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
                ));
            return (hashed, Convert.ToBase64String(salt));


        }
    }
}


