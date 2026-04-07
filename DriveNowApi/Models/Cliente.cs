using System.ComponentModel.DataAnnotations;

namespace DriveNowApi.Models;

public class Cliente
{
    public int Id { get; set; }
    [Required]
    public string Nome { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Cpf { get; set; }
}
