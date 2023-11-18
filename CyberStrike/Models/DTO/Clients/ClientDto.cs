using System.ComponentModel.DataAnnotations;

namespace CyberStrike.Models.DTO.Clients;

public class ClientDto
{
    [Required(ErrorMessage = "Campo email obrigatório!")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Campo passaword obrigatório!")]
    public string Password { get; set; }
}