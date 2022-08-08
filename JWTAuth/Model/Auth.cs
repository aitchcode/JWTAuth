using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTAuth.Model
{
    public class Auth : IJwtAuth
    {
        private readonly string username = "hammad";
        private readonly string password = "123456";
        private readonly string key;
        
        public Auth(string key)
        {
            this.key = key;
        }
        
        public string Authentication(string username, string password)
        {
            if (!(username.Equals(this.username)) || !(password.Equals(this.password)))
            {
                return null;
            }

            // Create Security Token Handler
            var tokenHandler = new JwtSecurityTokenHandler();

            // Create Private Key to Encrypted
            var tokenKey = Encoding.ASCII.GetBytes(key);

            // Create JETdescriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, username)
                    }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            
            // Create Token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Return Token from method
            return tokenHandler.WriteToken(token);
        }
    }
}
