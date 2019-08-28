using System;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Tabim.Core.Service.Jwt
{
    public static class JwtHelper
    {
        public static string BuildToken(string security_key, string issuer, string audience)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(security_key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                            issuer,
                            audience,
                            expires: DateTime.Now.AddMinutes(30),
                            signingCredentials: creds                            
                            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
