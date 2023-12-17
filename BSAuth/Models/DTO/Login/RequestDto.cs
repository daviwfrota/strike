using System.ComponentModel.DataAnnotations;

namespace BSAuth.Models.DTO.Login;

public class Request
{
    [Required(ErrorMessage = "Campo email é obrigatório!")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Campo password é obrigatório!")]

    public string Password { get; set; }
    public float Latitude { get; set; }
    public float Logintude { get; set; }
    
    public bool KeepAlive { get; set; }
}