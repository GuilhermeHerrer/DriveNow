using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DriveNowApi.Models;

public class Veiculo
{
    public int Id { get; set; }
    [Required]
    public string Nome { get; set; }
    [Required]
    public string Placa { get; set; }
    [Required]
    public string ValorDiaria { get; set; }
    public int AgenciaId { get; set; }
    public Agencia? Agencia { get; set; }

    public string? FotoUrl { get; set; }
}
