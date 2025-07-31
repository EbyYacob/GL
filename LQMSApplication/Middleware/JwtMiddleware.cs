using LQMSApplication.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LQMSApplication.Middleware
{

    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly IServiceScopeFactory _scopeFactory; //  Use Scope Factory

        public JwtMiddleware(RequestDelegate next, IConfiguration configuration, IServiceScopeFactory scopeFactory)
        {
            _next = next;
            _configuration = configuration;
            _scopeFactory = scopeFactory; //  Inject Scope Factory
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                using (var scope = _scopeFactory.CreateScope()) //  Create Scoped Service
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<LQMSDBContext>(); //  Get Scoped DbContext
                    var principal = await ValidateTokenAsync(token, dbContext);
                    if (principal == null)
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        await context.Response.WriteAsync("Unauthorized: Invalid or expired token");
                        return;
                    }

                    context.User = principal;
                }
            }

            await _next(context);
        }

        private async Task<ClaimsPrincipal?> ValidateTokenAsync(string token, LQMSDBContext _context)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = System.Text.Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]);

                var validationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["JwtSettings:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["JwtSettings:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

                var jwtToken = validatedToken as JwtSecurityToken;
                var userId = jwtToken?.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;

                if (userId == null)
                {
                    return null; // Invalid token
                }

                // Check token against database
                var user = await _context.T_SI_MAPP_User.FirstOrDefaultAsync(u => u.UserId == userId);
                if (user == null || user.CurrentJWT != token)
                {
                    return null; // Unauthorized, token mismatch
                }

                return principal;
            }
            catch
            {
                return null; // Token is invalid
            }
        }
    }

    #region To Do
    //public class JwtMiddleware
    //{
    //    private readonly RequestDelegate _next;
    //    private readonly string _secretKey;

    //    public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
    //    {
    //        _next = next;
    //        _secretKey = configuration["JwtSettings:key"];
    //    }

    //    public async Task Invoke(HttpContext context)
    //    {
    //        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

    //        if (token != null)
    //        {
    //            if (!AttachUserToContext(context, token))
    //            {
    //                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
    //                await context.Response.WriteAsync("Unauthorized: Invalid or expired token");
    //                return;
    //            }
    //        }

    //        await _next(context);
    //    }

    //    private bool AttachUserToContext(HttpContext context, string token)
    //    {
    //        try
    //        {
    //            var tokenHandler = new JwtSecurityTokenHandler();
    //            var key = Encoding.ASCII.GetBytes(_secretKey);

    //            var parameters = new TokenValidationParameters
    //            {
    //                ValidateIssuerSigningKey = true,
    //                IssuerSigningKey = new SymmetricSecurityKey(key),
    //                ValidateIssuer = false,
    //                ValidateAudience = false,
    //                ClockSkew = TimeSpan.Zero
    //            };

    //            var principal = tokenHandler.ValidateToken(token, parameters, out SecurityToken validatedToken);
    //            context.User = principal; // Attach user info to context
    //            return true;
    //        }
    //        catch
    //        {
    //            return false; // Token is invalid
    //            // If token validation fails, don't attach anything to context
    //        }
    //    }
    //}
    #endregion
}
