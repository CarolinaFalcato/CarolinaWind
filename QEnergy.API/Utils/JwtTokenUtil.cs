using Microsoft.IdentityModel.Tokens;
using QEnergy.Core.Domain.Configuration;
using QEnergy.Core.Domain.Models.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QEnergy.API.Utils
{
    public static class JwtTokenUtil
    {
        public static string UserIdClaim => "UserId";
        public static string CompanyIdClaim => "CompanyId";

        public static string GenerateJWTToken(UserModel userModel, JwtAuthorization configuration, ILogger logger)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration.SecretKey);

            var claims = new List<Claim>()
            {
                new Claim(UserIdClaim, userModel.Id.ToString()),
                new Claim(ClaimTypes.Name, userModel.Username.ToString()),
            };
            if (!string.IsNullOrEmpty(userModel.Name?.Trim()))
                claims.Add(new Claim(ClaimTypes.GivenName, string.Format("{0} {1}", userModel.Name, userModel.Surname).Trim()));
            if (!string.IsNullOrEmpty(userModel.Email?.Trim()))
                claims.Add(new Claim(ClaimTypes.Email, userModel.Email));
            if (userModel.CompanyId != default)
                claims.Add(new Claim(CompanyIdClaim, userModel.CompanyId.ToString()));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(configuration.ExpireInDays),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = configuration.Audience,
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
