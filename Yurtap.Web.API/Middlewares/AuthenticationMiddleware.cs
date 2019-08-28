using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace Yurtap.Web.API.Middlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            string autHeader = httpContext.Request.Headers["Authorization"];
            if (autHeader == null)
            {
                await _next(httpContext);
                return;
            }
            if (autHeader != null && autHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                var token = autHeader.Substring(7).Trim();
                var tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken decodeToken = null;
                try
                {
                    decodeToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
                }
                catch
                {
                    httpContext.Response.StatusCode = 500;
                }

                var claims = new[]
                {
                    new Claim("id",decodeToken.Claims.First(claim => claim.Type == "id").Value),
                    new Claim("name",decodeToken.Claims.First(claim => claim.Type == "name").Value),
                    new Claim("fullName",decodeToken.Claims.First(claim => claim.Type == "fullName").Value),
                    //new Claim(ClaimTypes.Role,"Personel"),
                    //new Claim(ClaimTypes.Role,"Ogrenci")
                };
                var identity = new ClaimsIdentity(claims, "Token");
                var principal = new ClaimsPrincipal(identity);
                httpContext.User = principal;
                Thread.CurrentPrincipal = principal;
            }
            else
            {
                httpContext.Response.StatusCode = 401;
            }

            await _next(httpContext);
        }
    }
}
