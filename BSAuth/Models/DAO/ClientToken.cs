using System.ComponentModel.DataAnnotations.Schema;
using BSAuth.Models.DAO.Generics;
using BSAuth.Models.Enums;

namespace BSAuth.Models.DAO;
[Table("client_tokens")]
public class ClientToken : Base
{
    public string Token { get; set; }
    public Client Client { get; set; }
    public TokenType Type { get; set; }
    public bool Revoked { get; set; } = false;
    
    public ClientToken() {}

    public ClientToken(string token, TokenType type, Client client)
    {
        Token = token;
        Type = type;
        Client = client;
    }

    public void Revoke()
    {
        Revoked = true;
    }
    
    public void ActivateIfIsRevoked()
    {
        if(Revoked)
            Revoked = false;
    }

    public void UpdateToken(string token)
    {
        Token = token;
    }
}