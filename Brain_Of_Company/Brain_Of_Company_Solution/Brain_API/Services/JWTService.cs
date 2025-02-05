


using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Brain_API.Services;
public class JwtService
{
        private readonly IConfiguration _config;

        public JwtService(IConfiguration configuration)
        {
            this._config = configuration;
        }
        public string GenerateJSONWebToken<T>(T admin /*, string roleId, string roleName*/ ) where T : class
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> userClaims = new List<Claim>();

            // Access user properties dynamically

            var userSSNProperty = typeof(T).GetProperty("SSN");
            var userNameProperty = typeof(T).GetProperty("Name");
            if (userSSNProperty != null && userNameProperty != null)
            {
                userClaims.Add(new Claim("SSN", userSSNProperty.GetValue(admin).ToString())); // Custom claim for ID
                userClaims.Add(new Claim("name", userNameProperty.GetValue(admin).ToString())); // Custom claim for Name
            }
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

}

