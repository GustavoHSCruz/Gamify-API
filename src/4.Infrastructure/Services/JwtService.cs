using Domain.Core.Entities;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Services
{
    public class JwtService : IJwtService
    {
        private readonly JwtOptions _opts;
        public JwtService(IOptions<JwtOptions> opts) => _opts = opts.Value;

        public string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_opts.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<System.Security.Claims.Claim>
            {
                new("sub", user.Id.ToString()),
                new("pid", user.PersonId.ToString()),
                new("ac", user.Email),
                new("fname", user.Person.FirstName),
                new("lname", user.Person.LastName),
            };
            // roles
            //claims.AddRange(user.Roles.Select(r => new System.Security.Claims.Claim("role", r)));

            var jwt = new JwtSecurityToken(
                issuer: _opts.Issuer,
                audience: _opts.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(_opts.ExpirationMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];

            using var rng = RandomNumberGenerator.Create();

            rng.GetBytes(randomNumber);

            return WebEncoders.Base64UrlEncode(randomNumber);
        }
    }

    public sealed class JwtOptions
    {
        public string Issuer { get; init; } = default!;
        public string Audience { get; init; } = default!;
        public string Key { get; init; } = default!;
        public int ExpirationMinutes { get; init; }
    }
}