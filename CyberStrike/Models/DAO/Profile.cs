using System.ComponentModel.DataAnnotations.Schema;
using CyberStrike.Models.DAO.Generics;
using CyberStrike.Models.Enums;

namespace CyberStrike.Models.DAO;
[Table("client_profile")]
public class ClientProfile : Base
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDay { get; set; }
    public Genres Genre { get; set; } = Genres.DOESNT_MEAN;
    public string Contact { get; set; }
    public Client Client { get; set; }
    public Guid ClientId { get; set; }
    public ClientProfile() {}
}