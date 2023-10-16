using System.ComponentModel.DataAnnotations.Schema;
using CyberStrike.Models.DAO.Bases;

namespace CyberStrike.Models.DAO;
[Table("profiles")]
public class Profile : Base
{
    public string Name { get; set; }
    public string? Bio { get; set; }
    public string? Avatar { get; set; }
    public string? BirthDate { get; set; }
    public User User { get; set; }
}