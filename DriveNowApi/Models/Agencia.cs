using System.ComponentModel.DataAnnotations;
namespace DriveNowApi.Models;

public class Agencia
{
    public int Id { get; set; }
    [Required]
    public string NomeFantasia { get; set; }
    [Required]
    public string Cep { get; set; }
    public string? Logradouro { get; set; }
    public string? Bairro { get; set; }
    public string? Localidade { get; set; }
    public string? Uf { get; set; }
}
