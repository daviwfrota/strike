using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace BSAuth.Utils;

public class GenerateJwt
{
    public string Jwt { get; set; }
    public GenerateJwt(int expireIn, string secret, IEnumerable<Claim> claims)
    {

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secret));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims, 
            expires: DateTime.Now.AddMinutes(expireIn),
            signingCredentials: cred);
        Jwt = new JwtSecurityTokenHandler().WriteToken(token);
    }
}