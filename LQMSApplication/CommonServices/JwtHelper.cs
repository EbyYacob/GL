using LQMSApplication.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LQMSApplication.CommonServices
{
    public class JwtHelper
    {
        private readonly IConfiguration _configuration;
        private readonly LQMSDBContext _context;

        public JwtHelper(IConfiguration configuration, LQMSDBContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        public string GenerateToken(string userId, string username)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.UniqueName, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                //expires: DateTime.UtcNow.AddHours(1), // Token expires in 1 hour
                //expires: DateTime.UtcNow.AddDays(30), // Token expires in 30 days
                expires: DateTime.UtcNow.AddYears(1), // 1 year
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsPrincipal? ValidateToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]);

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["JwtSettings:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["JwtSettings:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero // No tolerance for expired tokens
                };

                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

                return principal;
            }
            catch
            {
                return null; // Token is invalid
            }
        }

        #region To Do
        //public async Task<ClaimsPrincipal?> ValidateTokenAsync(string token)
        //{
        //    try
        //    {
        //        var tokenHandler = new JwtSecurityTokenHandler();
        //        var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]);

        //        var validationParameters = new TokenValidationParameters
        //        {
        //            ValidateIssuerSigningKey = true,
        //            IssuerSigningKey = new SymmetricSecurityKey(key),
        //            ValidateIssuer = true,
        //            ValidIssuer = _configuration["JwtSettings:Issuer"],
        //            ValidateAudience = true,
        //            ValidAudience = _configuration["JwtSettings:Audience"],
        //            ValidateLifetime = true,
        //            ClockSkew = TimeSpan.Zero // No tolerance for expired tokens
        //        };

        //        SecurityToken validatedToken;
        //        var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

        //        var jwtToken = validatedToken as JwtSecurityToken;
        //        var userId = jwtToken?.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;

        //        if (userId == null)
        //        {
        //            return null; // Invalid token
        //        }

        //        // Check token against database
        //        var user = await _context.T_SI_MAPP_User.FirstOrDefaultAsync(u => u.UserId == userId);
        //        if (user == null || user.CurrentJWT != token)
        //        {
        //            return null; // Unauthorized, token mismatch
        //        }

        //        return principal;
        //    }
        //    catch
        //    {
        //        return null; // Token is invalid
        //    }
        //}
        #endregion
    }
}
