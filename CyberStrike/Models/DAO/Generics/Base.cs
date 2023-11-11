using System.ComponentModel.DataAnnotations;

namespace CyberStrike.Models.DAO.Bases;

public class Base
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public bool Active { get; set; } = true;

    public void Deactive()
    {
        Active = false;
    }
    
    public void Activate()
    {
        Active = true;
    }
}