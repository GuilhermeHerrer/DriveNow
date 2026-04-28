using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DriveNowApi.DTOs
{
    public class VeiculoCreateDTO
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Placa { get; set; }
        [Required]
        public string ValorDiaria { get; set; }
        [Required]
        public int AgenciaId { get; set; }
        public string? FotoUrl { get; set; }
    }
}
