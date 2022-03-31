using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace JWTDemo
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        //instead of  data base create a dictionary 
        private readonly IDictionary<string, string> users=new Dictionary<string, string>
            {{ "test1","pass1" },{ "test2","pass2" }};
        private string key;

        public JwtAuthenticationManager(string key)
        {
            this.key = key;
        }
    public string Authenticate(string username, string password)
        {
            if(!users.Any(u =>u.Key==username && u.Value == password))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenkey = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenkey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

  
    }
}
