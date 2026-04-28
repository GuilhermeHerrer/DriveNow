using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    public string? FotoUrl { get; set; }
}
