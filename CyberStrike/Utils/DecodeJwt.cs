using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CyberStrike.Utils;

public class DecodeJwt
{
    public string Email { get; set; }
    public DecodeJwt(string token)
    {
        
        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(token);
        Email = jwtSecurityToken.Claims.First(c => c.Type == ClaimTypes.Email).Value;
    }
}