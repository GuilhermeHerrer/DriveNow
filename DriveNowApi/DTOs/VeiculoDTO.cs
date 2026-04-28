using System.ComponentModel.DataAnnotations;

namespace DriveNowApi.DTOs
{
    public class VeiculoDTO
    {
        public int id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Placa { get; set; }
        [Required]
        public string ValorDiaria { get; set; }
        public string NomeAgencia { get; set; }
        public string? FotoUrl { get; set; }
    }
}
