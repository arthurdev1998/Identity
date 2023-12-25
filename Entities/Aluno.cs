using System.ComponentModel.DataAnnotations;

namespace Identity.Entities;

public class Aluno
{
    [Key]
    public int AlunoId { get; set; }

    [Required, MaxLength(80, ErrorMessage = "Nome nao pode exceder 80 caracteres")]
    public required string Nome { get; set; }

    [EmailAddress]
    [Required, MaxLength(120)]
    public required string Email { get; set; }

    public int Idade { get; set; }

    [MaxLength(80)]
    public string? curso { get; set; }
}