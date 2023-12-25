using System.ComponentModel.DataAnnotations;

namespace Identity.Models;
public class LoginViewModel
{
    [Required(ErrorMessage = "Email obrigatório PORRRRA")]
    [EmailAddress(ErrorMessage = "Email Inválido")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "SENHA obrigatória PORRRRA")]
    [DataType(DataType.Password)]
    public required string Password { get; set; }

    [Display(Name = "Lembrar-me")]
    public bool RememberMe { get; set; }
}