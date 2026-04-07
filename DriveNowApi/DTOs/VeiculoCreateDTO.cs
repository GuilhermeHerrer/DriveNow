using System.ComponentModel.DataAnnotations;

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
        public string AgenciaId { get; set; }
    }
}
