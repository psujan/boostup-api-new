﻿using Boostup.API.Entities;
using Boostup.API.Interfaces.Auth;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Boostup.API.Repositories.Auth
{
    public class TokenRepository :ITokenRepository
    {
        private readonly IConfiguration configuration;

        private static readonly int TokenExpiresIn = 540; // 9 hrs

        public TokenRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string CreateJWTToken(User user, List<string> roles)
        {

            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                 configuration["Jwt:Audience"],
                 claims,
                 expires: DateTime.Now.AddMinutes(TokenExpiresIn),
                 signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
