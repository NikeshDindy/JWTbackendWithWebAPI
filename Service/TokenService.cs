using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTbackendWithWebAPI.Service
{
    public class TokenService
    {
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(string userName,string email,string role)
        {
            var jwtSettings = _config.GetSection("Jwt");

            // creating a symmetric security key - (raw key)
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));

            // creating sigining credentials - (raw key + algo)
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);
            //var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name,userName),
                new Claim(ClaimTypes.Email,email),
                new Claim(ClaimTypes.Role,role ?? "User"),
                new Claim("Department","HR-Department"),
                new Claim("Age","50")
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpireMinutes"])),
                signingCredentials: creds
            );

            Console.WriteLine("TokenService Issuer: " + jwtSettings["Issuer"]);
            Console.WriteLine("TokenService Audience: " + jwtSettings["Audience"]);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
