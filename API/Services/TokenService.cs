using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    // we need to inject the configurations as these will contain the key for
    // token
    public class TokenService(IConfiguration config) : ITokenService
    {
        public string CreateToken(AppUser user)
        {
            //accessing the token key inserted in configuration 
            var TokenKey = config["TokenKey"] ?? throw new Exception("Cannot access token key from appsettings.");
            // if shorter than 64 an excveption would be thrown anyway later
            if(TokenKey.Length < 64) throw new Exception("Token key is too short.");
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenKey));
            
            //when user make a request they make claims, that is something they declare
            // for example in this case their username
            var claims = new List<Claim> {
                new(ClaimTypes.NameIdentifier, user.UserName)
            };
            // create credentials and set the algorithm that will encrypt them
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            // creating a decriptor for the token with the claims as subject
            // and expiry date and credentials
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = creds
            };
            // token handler is responsible for creating the token and writing it
            // this comes from a nuget package you need to install
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            // this will serialize the token
            return tokenHandler.WriteToken(token);


        }

    }
}