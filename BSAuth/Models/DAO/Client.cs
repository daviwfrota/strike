using System.ComponentModel.DataAnnotations.Schema;
using BSAuth.Models.DAO.Generics;
using BC = BCrypt.Net.BCrypt;

namespace BSAuth.Models.DAO;
[Table("clients")]
public class Client : Base
{
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime LastCommunication { get; set; }
    public bool IsVerified { get; set; } = false;
    public ClientProfile Profile { get; set; } = new ClientProfile();
    public Guid ProfileId { get; set; }
    public List<ClientLocation> ClientLocations { get; set; } = new List<ClientLocation>();
    public List<ClientToken> ClientTokens { get; set; } = new List<ClientToken>();

    public Client(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public void Hash()
    {
        Password = BC.HashPassword(Password);
    }

    public bool VerifyPassword(string password)
    {
        return BC.Verify(password, Password);
    }

    public void UpdateLastCommunication()
    {
        LastCommunication = DateTime.Now;
    }

    public void Verify()
    {
        IsVerified = true;
    }
}