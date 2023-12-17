using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BSAuth.Utils;

public class DecodeJwt
{
    public string Email { get; set; }
    public bool isValid { get; set; }
    public DecodeJwt(string token)
    {
        
        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(token);
        Email = jwtSecurityToken.Claims.First(c => c.Type == ClaimTypes.Email).Value;
        isValid = jwtSecurityToken.ValidTo > DateTime.Now;
    }
}