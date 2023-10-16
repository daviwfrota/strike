using System.ComponentModel.DataAnnotations.Schema;
using CyberStrike.Models.DAO.Bases;
using CyberStrike.Models.Enums;
using BC = BCrypt.Net.BCrypt;

namespace CyberStrike.Models.DAO;
[Table("users")]
public class User : Base
{
    public string Email { get; set; }
    public string Password { get; set; }
    public UserType Type { get; set; }
    public Profile? Profile { get; set; }

    public User(string email, string password, UserType type = UserType.Client)
    {
        Email = email;
        Password = password;
        Type = type;
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