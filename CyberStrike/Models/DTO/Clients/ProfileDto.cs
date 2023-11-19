using CyberStrike.Models.Enums;

namespace CyberStrike.Models.DTO.Clients;

public class ClientProfileDto
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDay { get; set; }
    public Genres Genre { get; set; } = Genres.DOESNT_MEAN;
    public string Contact { get; set; }
}