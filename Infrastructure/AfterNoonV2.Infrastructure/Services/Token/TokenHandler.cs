using AfterNoonV2.Application.Abstractions.Token;
using AfterNoonV2.Domain.Entities.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using D = AfterNoonV2.Application.DTOs;

namespace AfterNoonV2.Infrastructure.Services.Token
{
    public class TokenHandler : ITokenHandler
    {
        readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public D.Token CreateAccessToken(AppUser user)
        {
            D.Token token = new();

            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_configuration["Token:SigningKey"]));

            // Creating hashed identity
            SigningCredentials signingCredentials = new(key, SecurityAlgorithms.HmacSha256);

            // Giving options
            token.Expartion = DateTime.UtcNow.AddMinutes(20);

            JwtSecurityToken jwt = new(
                audience: _configuration["Token:Audience"],
                issuer: _configuration["Token:Issuer"],
                expires: token.Expartion,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials,
                claims: new List<Claim> { new(ClaimTypes.Name, user.UserName) }
                );

            // Geting example from token craetor
            JwtSecurityTokenHandler handler = new();
            token.AccessToken = handler.WriteToken(jwt);

            token.RefreshToken = CreateRefreshToken();

            return token;
        }

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);

            return Convert.ToBase64String(number);
        }
    }
}
