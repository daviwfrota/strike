using System.ComponentModel.DataAnnotations.Schema;
using CyberStrike.Models.DAO.Bases;
using BC = BCrypt.Net.BCrypt;

namespace CyberStrike.Models.DAO;
[Table("clients")]
public class Client : Base
{
    public string Email { get; set; }
    public string Password { get; set; }

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
}