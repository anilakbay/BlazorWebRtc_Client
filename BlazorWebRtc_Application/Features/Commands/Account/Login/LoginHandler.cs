using BlazorWebRtc_Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace BlazorWebRtc_Application.Features.Commands.Account.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, (bool, string)>
    {
        private readonly AppDbContext _context;
        public LoginHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(bool, string)> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName == request.UserName, cancellationToken);

            if (user == null || !VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return (false, string.Empty);
            }

            var token = "";
            return token;


        }

        private bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            byte[] salt = Convert.FromBase64String(storedSalt);


            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
                ));
            return hashed == storedHash;


        }
    }
}
