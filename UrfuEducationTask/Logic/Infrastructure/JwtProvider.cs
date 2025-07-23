using Logic.Models;
using Logic.Models.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Dal.Models;
using Logic.Model;


namespace Logic.Infrastructure
{
    public class JwtProvider : IJwtProvider
    {
        public string GenerateToken(UserModel user, string key, string issuer, int expiresHours)
        {
            var claims = new Claim[]
            {
                new(ClaimTypes.MobilePhone, user.Phone),
            };
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), 
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience : issuer,
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddHours(expiresHours));
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool IsTokenValid(string key, string issuer, string token)
        {
            var mySecret = Encoding.UTF8.GetBytes(key);
            var mySecurityKey = new SymmetricSecurityKey(mySecret);
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token,
                    new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = issuer,
                        ValidAudience = issuer,
                        IssuerSigningKey = mySecurityKey,
                    }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
