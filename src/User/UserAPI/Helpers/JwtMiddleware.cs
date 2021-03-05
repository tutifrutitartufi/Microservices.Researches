using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAPI.Repositories.Interfaces;
using UserAPI.Settings;

namespace UserAPI.Helpers
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAuthSettings _authSettings;

        public JwtMiddleware(RequestDelegate next, IAuthSettings authSettings)
        {
            _next = next;
            _authSettings = authSettings;
        }

        public async Task Invoke(HttpContext context, IUserRepository repository)
       {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            bool sameOrigin = context.Request.Headers["same-origin"] == "1";

            if (sameOrigin)
            {
                context.Items["Same-Origin"] = true;
            }

            if (token != null)
                attachUserToContext(context, repository, token);

            await _next(context);
        }

        private async void attachUserToContext(HttpContext context, IUserRepository repository, string token)
        {
            bool sameOrigin = context.Request.Headers["same-origin"] == "1";
            if (sameOrigin)
            {
                context.Items["SameOrigin"] = true;
            }
            else
            {
                try
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes("Zivot tezak nije lak................");
                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                        ClockSkew = TimeSpan.Zero
                    }, out SecurityToken validatedToken);

                    var jwtToken = (JwtSecurityToken)validatedToken;
                    var userId = jwtToken.Claims.First(x => x.Type == "id").Value;

                    // attach user to context on successful jwt validation
                    context.Items["User"] = await repository.GetUser(userId);
                }
                catch
                {
                    // do nothing if jwt validation fails
                    // user is not attached to context so request won't have access to secure routes
                }
            }
            
        }
    }
}
